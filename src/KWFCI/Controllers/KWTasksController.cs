using KWFCI.Models;
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

        public KWTasksController(IKWTaskRepository repo, IStaffProfileRepository repo2)
        {
            taskRepo = repo;
            staffRepo = repo2;
        }
        //[Route("Index")]
        public IActionResult AllKWTasks()
        {
            var vm = new KWTaskVM();
            vm.StaffList = staffRepo.GetAllStaffProfiles().ToList();
            vm.KWTasks = taskRepo.GetAllKWTasks().ToList();
            return View(vm);
        }

        [Route("Add")]
        [HttpPost]
        public IActionResult AddKWTask(KWTask kwt)
        {
            var kwtask = new KWTask
            {
                Message = kwt.Message,
                AlertDate = kwt.AlertDate,
                DateCreated = kwt.DateCreated,
                DateDue = kwt.DateDue,
                Priority = kwt.Priority
            };

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
        public ActionResult Edit(KWTaskVM vm)
        {
            KWTask kwtask = taskRepo.GetKWTaskByID(vm.NewKWTask.KWTaskID);
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

    }
}
