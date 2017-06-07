using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KWFCI.Models;
using Microsoft.AspNetCore.Identity;
using KWFCI.Repositories;
using KWFCI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KWFCI.Controllers
{
    
    [Route("Index")]
    [Route("/")]
    [Authorize(Roles = "Staff")]
    public class HomeController : Controller
    {
        private UserManager<StaffUser> userManager;
        private IStaffProfileRepository staffProfRepo;
        private IKWTaskRepository taskRepo;
        private IInteractionsRepository intRepo;
        private IBrokerRepository brokerRepo;
        

        public HomeController(UserManager<StaffUser> usrMgr, IStaffProfileRepository repo, IKWTaskRepository repo2, IInteractionsRepository repo3, IBrokerRepository repo4)
        {
            staffProfRepo = repo;
            userManager = usrMgr;
            taskRepo = repo2;
            intRepo = repo3;
            brokerRepo = repo4;
        }
        
        public async Task <IActionResult> Index()
        {
            /*Login Logic*/
            StaffUser user = await userManager.FindByNameAsync(User.Identity.Name);
            Helper.StaffUserLoggedIn = user;
            Helper.StaffProfileLoggedIn = Helper.DetermineProfile(staffProfRepo);
            /*End Login Logic*/

            /*Call the GetToday method of the helper class to get and set today's date for use with datepicker validation*/
            Helper.GetToday();

            /* Priority reassignment Logic */
            var tasks = taskRepo.GetAllKWTasks().Where(t => t.Type != "Onboarding");

            
            foreach (KWTask t in tasks.ToList())
            {
                int diff = (int)(t.DateDue - DateTime.Today).Value.TotalDays;
                if (diff < 0 && t.Priority < 5)
                {
                    StaffProfile profile = staffProfRepo.GetProfileByTask(t);

                    t.Priority = 5;
                    taskRepo.UpdateKWTask(t);

                    if (profile != null)
                    {
                        profile.Tasks.Remove(t);
                        staffProfRepo.UpdateStaff(profile);
                    }
                }
                else if (diff == 0 && t.Priority < 4)
                {
                    t.Priority = 4;
                    taskRepo.UpdateKWTask(t);
                }

                else if (diff <= 2 && t.Priority < 3)
                {
                    t.Priority = 3;
                    taskRepo.UpdateKWTask(t);
                }

                else if (diff == 3 && t.Priority < 2)
                {
                    t.Priority = 2;
                    taskRepo.UpdateKWTask(t);
                }
            }

            

            /*Display Alerts Logic*/
            ViewBag.Name = Helper.StaffProfileLoggedIn.FirstName;
            ViewBag.Alerts = Helper.StaffProfileLoggedIn.Tasks.Where(
                t => t.Type == "Alert" && 
                (DateTime.Compare(DateTime.Now, t.AlertDate.GetValueOrDefault()) == 0 ||
                DateTime.Compare(DateTime.Now, t.AlertDate.GetValueOrDefault()) > 0)).ToList();

            var criticalVB = taskRepo.GetAllTasksByType("Alert").Where(t => t.Priority == 5).ToList();

            foreach(KWTask task in taskRepo.GetAllTasksByType("Task").Where(t => t.Priority == 5).ToList())
            {
                criticalVB.Add(task);
            }

            ViewBag.Critical = criticalVB;
             
            /*End Display Alerts Logic*/

            /*Populate ViewModel Logic*/
            var vm = new HomeVM();
            
            /*TODO Be aware this directly queries the database, may break if StaffProfile or KWTask models change*/
            string sqlGlobal = "SELECT * FROM dbo.KWTasks WHERE StaffProfileID IS NULL";
            string sqlPersonal = "SELECT * FROM dbo.KWTasks WHERE StaffProfileID = " + Helper.StaffProfileLoggedIn.StaffProfileID;
            vm.GlobalTasks = taskRepo.GetTasksFromSQL(sqlGlobal).Where(t => t.Type != "Onboarding").ToList();
            vm.PersonalTasks = taskRepo.GetTasksFromSQL(sqlPersonal).Where(t => t.Type != "Onboarding").ToList();

            List<Broker> brokers = new List<Broker>();
            foreach(Interaction i in Helper.StaffProfileLoggedIn.Interactions)
            {
                foreach (Broker b in brokerRepo.GetAllBrokers())
                {
                    if(b.Interactions.Contains(i))
                    {
                        i.BrokerName = b.FirstName + " " + b.LastName;

                        if (!brokers.Contains(b))
                            brokers.Add(b);
                    }
                }
            }
            ViewBag.Brokers = brokers;


            vm.PersonalInteractions = Helper.StaffProfileLoggedIn.Interactions;
            vm.NewTask = new KWTask();
            vm.NewBroker = new Broker();
            vm.NewMessage = new Message();
            vm.StaffList = staffProfRepo.GetAllStaffProfiles().ToList();
            /*End Populate ViewModel Logic*/

            return View(vm);
        }
    }
}
