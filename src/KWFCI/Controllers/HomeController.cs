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

            /*Display Alerts Logic*/
            ViewBag.Name = Helper.StaffProfileLoggedIn.FirstName;
            ViewBag.Alerts = Helper.StaffProfileLoggedIn.Tasks.Where(
                t => t.Type == "Alert" && 
                (DateTime.Compare(DateTime.Now, t.AlertDate.GetValueOrDefault()) == 0 ||
                DateTime.Compare(DateTime.Now, t.AlertDate.GetValueOrDefault()) > 0)).ToList();

            ViewBag.Critical = taskRepo.GetAllTasksByType("Alert").Where(t => t.Priority == 5).ToList();
            /*End Display Alerts Logic*/

            /*Populate ViewModel Logic*/
            var vm = new HomeVM();
            
            string sql = "SELECT * FROM dbo.KWTasks WHERE StaffProfileID IS NULL";
            vm.GlobalTasks = taskRepo.GetTasksFromSQL(sql).Where(t => t.Type != "Onboarding").ToList();
            vm.PersonalTasks = new List<KWTask>();
            vm.PersonalInteractions = new List<Interaction>();
            vm.NewTask = new KWTask();
            vm.NewBroker = new Broker();
            /*End Populate ViewModel Logic*/

            return View(vm);
        }

        //public IActionResult HomePageMyTasks(List<KWTask> personalTasks)
        //{
        //    return View("index");
        //}
    }
}
