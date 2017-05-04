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

        public KWTasksController(IKWTaskRepository repo)
        {
            taskRepo = repo;
        }

        public IActionResult AllKWTasks()
        {
            var allKWTasks = taskRepo.GetAllKWTasks().ToList();
            var vm = new KWTaskVM();
            vm.KWTasks = allKWTasks;
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
        public ActionResult Edit(int id)
        {
            KWTask kwtask = taskRepo.GetKWTaskByID(id);
            if (kwtask != null)
            {
                return View(kwtask);
            }
            else
            {
                return RedirectToAction("AllKWTasks");
            }
        }

        [Route("Edit")]
        [HttpPost]
        public IActionResult Edit(KWTask kwt)
        {
            if (kwt != null)
            {
                KWTask kwtask = taskRepo.GetKWTaskByID(kwt.KWTaskID);
                kwtask.Message = kwt.Message;
                kwtask.AlertDate = kwt.AlertDate;
                //kwtask.DateCreated = kwt.DateCreated;
                kwtask.DateDue = kwt.DateDue;
                kwtask.Priority = kwt.Priority;

                int verify = taskRepo.UpdateKWTask(kwtask);
                if (verify == 1)
                {
                    //TODO add feedback of success
                    return RedirectToAction("AllKWTasks");
                }
                else
                {
                    //TODO add feedback for error
                }
            }
            else
            {
                ModelState.AddModelError("", "Task Not Found");
            }
            return View(kwt);
        }
    }
}
