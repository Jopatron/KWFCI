﻿using KWFCI.Models;
using KWFCI.Models.ViewModels;
using KWFCI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Controllers
{
    [Authorize(Roles = "Staff")]
    [Route("Tasks")]
    public class KWTasksController : Controller
    {
        private IKWTaskRepository taskRepo;
        private IStaffProfileRepository staffRepo;
        private IInteractionsRepository intRepo;

        public KWTasksController(IKWTaskRepository repo, IStaffProfileRepository repo2, IInteractionsRepository repo3)
        {
            taskRepo = repo;
            staffRepo = repo2;
            intRepo = repo3;
        }
        //[Route("Index")]
        public IActionResult AllKWTasks()
        {
            var vm = new KWTaskVM();
            vm.StaffList = staffRepo.GetAllStaffProfiles().ToList();
            vm.KWTasks = taskRepo.GetAllKWTasks().ToList();
            vm.NewKWTask = new KWTask();
            return View(vm);
        }

        [Route("Add")]
        [HttpPost]
        public IActionResult AddKWTask(KWTaskVM vm, int? staffProfileID)
        {
            var kwtask = new KWTask
            {
                Message = vm.NewKWTask.Message,
                AlertDate = vm.NewKWTask.AlertDate,
                DateCreated = vm.NewKWTask.DateCreated,
                DateDue = vm.NewKWTask.DateDue,
                Priority = vm.NewKWTask.Priority
            };
            if (kwtask.AlertDate == null)
                kwtask.Type = "Task";
            else
                kwtask.Type = "Alert";

            taskRepo.AddKWTask(kwtask);
            //TODO: See if there is a way to just close the modal and not refresh the page
            return RedirectToAction("AllKWTasks");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            KWTask kwtask = taskRepo.GetKWTaskByID(id);
            if (kwtask != null)
            {
                var interaction = taskRepo.GetAssociatedInteraction(kwtask);
                if (interaction != null)
                {
                    interaction.Task = null;
                    interaction.TaskForeignKey = null;
                    intRepo.UpdateInteraction(interaction);
                }
                    
                taskRepo.DeleteKWTask(kwtask);
                return RedirectToAction("AllKWTasks");
            }
            else
            {
                ModelState.AddModelError("", "Task Not Found");
            }
            return RedirectToAction("AllKWTasks");
        }

        [Route("Edit")]
        [HttpPost]
        public ActionResult Edit(KWTaskVM vm, int KWTaskID)
        {
            KWTask kwtask = taskRepo.GetKWTaskByID(KWTaskID);
            kwtask.Message = vm.NewKWTask.Message;
            kwtask.AlertDate = vm.NewKWTask.AlertDate;
            kwtask.DateDue = vm.NewKWTask.DateDue;
            kwtask.Priority = vm.NewKWTask.Priority;
            kwtask.Type = vm.NewKWTask.Type;

            if (kwtask.AlertDate == null)
                kwtask.Type = "Task";
            else
                kwtask.Type = "Alert";

            int verify = taskRepo.UpdateKWTask(kwtask);
            if (verify == 1)
            {
                return RedirectToAction("AllKWTasks");
            }
            else
            {
                ModelState.AddModelError("", "Task Not Found");
            }
            return RedirectToAction("AllKWTasks");
        }

        [Route("Assign")]
        [HttpPost]
        public ActionResult Assign(string StaffProfileName, int KWTaskID)
        {
            bool verify;
            var task = taskRepo.GetKWTaskByID(KWTaskID);
            var allProfiles = staffRepo.GetAllStaffProfiles().ToList();
            if (StaffProfileName != "clear")
            {
                string[] name = StaffProfileName.Split(' ');
                var profile = staffRepo.GetStaffProfileByFullName(name[0], name[1]) as StaffProfile;
                verify = ProcessAssign(task, allProfiles, profile);

                if (verify)
                {
                    return RedirectToAction("AllKWTasks");
                }
                else
                {
                    ModelState.AddModelError("", "Task Not Found");
                }
            }
            else
            {
                verify = ProcessAssign(task, allProfiles, true);
            }
            
            return RedirectToAction("AllKWTasks");
        }


        //First unassigns the task from staff, then assigns it, then updates the repo
        private bool ProcessAssign(KWTask t, List<StaffProfile> sps, StaffProfile staff)
        {
            foreach(StaffProfile sp in sps)
            {
                if (sp.Tasks.Contains(t))
                    sp.Tasks.Remove(t);
            }
            staff.Tasks.Add(t);
            int verify = staffRepo.UpdateStaff(staff);
            if (verify > 0)
                return true;
            else
                return false;
        }

        private bool ProcessAssign(KWTask t, List<StaffProfile> sps, bool clear)
        {
            foreach (StaffProfile sp in sps)
            {
                if (sp.Tasks.Contains(t))
                {
                    sp.Tasks.Remove(t);
                    staffRepo.UpdateStaff(sp);
                }
            }
            return true;
        }
    }
}
