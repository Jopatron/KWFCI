using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using KWFCI.Repositories;
using KWFCI.Models;
using KWFCI.Models.ViewModels;

namespace KWFCI.Controllers
{
    [Route("Interactions")]
    public class InteractionsController : Controller
    {
        private IInteractionsRepository intRepo;
        private IBrokerRepository brokerRepo;
        private IStaffProfileRepository staffRepo;
        private IKWTaskRepository taskRepo;

        public InteractionsController(IInteractionsRepository repo, IBrokerRepository repo2, IStaffProfileRepository repo3, IKWTaskRepository repo4)
        {
            intRepo = repo;
            brokerRepo = repo2;
            staffRepo = repo3;
            taskRepo = repo4;
        }
        
        [Route("Brokers")]
        public IActionResult BrokerInteractions(int BrokerID)
        {
            var broker = brokerRepo.GetBrokerByID(BrokerID);
            ViewBag.BrokerName = broker.FirstName + " " + broker.LastName;
            ViewBag.StaffEmail = Helper.StaffProfileLoggedIn.Email;
            var allInteractions = broker.Interactions; //This is where the issue seems to present
            var vm = new InteractionVM();
            vm.Interactions = allInteractions;
            vm.Broker = broker;
            vm.NewInteraction = new Interaction();
            vm.Task = new KWTask();
            vm.TasksCompleted = 0;


            List<KWTask> tasks = new List<KWTask>();
            foreach (KWTask t in broker.Requirements)
            {
                tasks.Add(t);
                if (t.IsComplete)
                    vm.TasksCompleted++;
                
            }
            ViewBag.Percent = Math.Round((vm.TasksCompleted / 16) * 100);

            foreach(Interaction i in broker.Interactions)
            {
                if(i.TaskForeignKey != null)
                    tasks.Add(taskRepo.GetKWTaskByID((int)i.TaskForeignKey));
            }
            vm.Tasks = tasks;
            //TODO Ensure user is rerouted if not logged in
            return View(vm);
        }
        [Route("Staff")]
        public IActionResult StaffInteractions()
        {
            //TODO Ensure user is rerouted if not logged in
            return View(intRepo.GetAllInteractions().ToList());
        }

        [HttpPost]
        public IActionResult Archive(int id)
        {
            Interaction i = intRepo.GetInteractionById(id);
            if (i != null)
            {
                intRepo.ChangeStatus(i, "Archived");
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return RedirectToAction("Index", intRepo.GetAllInteractions().ToList());
        }
        [Route("Add")]
        //[HttpPost]
        public IActionResult AddInteraction(int BrokerID)
        {
            var broker = brokerRepo.GetBrokerByID(BrokerID);

            var NewInteraction = new Interaction();
            
            broker.Interactions.Add(NewInteraction);
            
            var profile = staffRepo.GetStaffProfileByFullName(Helper.StaffProfileLoggedIn.FirstName, Helper.StaffProfileLoggedIn.LastName);
            profile.Interactions.Add(NewInteraction);


            intRepo.AddInteraction(NewInteraction);
            
            //TODO: See if there is a way to just close the modal and not refresh the page
            return RedirectToAction("BrokerInteractions", new {BrokerID = BrokerID });

        }

        [Route("Edit")]
        [HttpPost]
        public ActionResult Edit(InteractionVM iVM, string taskAction = "")
        {
            var i = iVM.NewInteraction;
            if (i != null)
            {
                Interaction interaction = intRepo.GetInteractionById(i.InteractionID);

                if (iVM.Field == "Notes")
                    interaction.Notes = i.Notes;
                else if (iVM.Field == "NextStep")
                {
                    if(iVM.Task != null && taskAction == "New")
                    {
                        KWTask task = new KWTask()
                        {
                            AlertDate = iVM.Task.AlertDate,
                            DateDue = iVM.Task.DateDue,
                            Message = iVM.Task.Message,
                            Priority = iVM.Task.Priority,
                            DateCreated = iVM.Task.DateCreated

                        };
                        if (task.AlertDate == null)
                            task.Type = "Task";
                        else
                            task.Type = "Alert";

                        var profile = staffRepo.GetStaffProfileByFullName(Helper.StaffProfileLoggedIn.FirstName, Helper.StaffProfileLoggedIn.LastName);
                        profile.Tasks.Add(task);
                        taskRepo.AddKWTask(task);
                        interaction.Task = task;
                        task.Interaction = interaction;

                        
                        taskRepo.UpdateKWTask(task);
                        

                    }  
                    else if(iVM.Task != null && taskAction == "Edit")
                    {
                        KWTask task = taskRepo.GetKWTaskByID(iVM.Task.KWTaskID);
                        task.Message = iVM.Task.Message;
                        task.AlertDate = iVM.Task.AlertDate;
                        task.DateDue = iVM.Task.DateDue;
                        task.Priority = iVM.Task.Priority;

                        taskRepo.UpdateKWTask(task);
                    }
                    else
                    {
                        interaction.NextStep = i.NextStep;
                    }
                        
                }
                    
                else if (iVM.Field == "Date Created")
                    interaction.DateCreated = i.DateCreated;
                
                
                
                int verify = intRepo.UpdateInteraction(interaction); //Repository and broker.Interactions reflect proper values here
                if (verify == 1)
                {
                    //TODO add feedback of success
                    return RedirectToAction("BrokerInteractions", new { BrokerID = iVM.BrokerID });

                }
                else
                {
                    //TODO add feedback for error
                }
            }
            else
            {
                ModelState.AddModelError("", "Interaction Not Found");
            }

            return RedirectToAction("BrokerInteractions", new { BrokerID = iVM.BrokerID});
        }
    }
}
