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
    [Route("KWTasks")]
    public class KWTasksController : Controller
    {
        private IKWTaskRepository kwtRepo;

        public KWTasksController(IKWTaskRepository repo)
        {
            kwtRepo = repo;
        }

        public IActionResult AllKWTasks()
        {
            var allKWTasks = kwtRepo.GetAllKWTasks().ToList();
            var vm = new KWTaskVM();
            vm.KWTasks = allKWTasks;
            return View(vm);
        }

        [Route("Add")]
        [HttpPost]
        public IActionResult AddKWTask(KWTaskVM vm)
        {
            var kwt = vm.NewKWTask;
            var kwtask = new KWTask
            {
                Description = kwt.Description,
                StaffProfile = kwt.StaffProfile,
                Alert = kwt.Alert
            };

            kwtRepo.AddKWTask(kwtask);
            //TODO: See if there is a way to just close the modal and not refresh the page
            return RedirectToAction("AllKWTasks");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            KWTask kwtask = kwtRepo.GetKWTaskByID(id);
            if (kwtask != null)
            {
                kwtRepo.DeleteKWTask(kwtask);
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
            KWTask kwtask = kwtRepo.GetKWTaskByID(id);
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
                KWTask kwtask = kwtRepo.GetKWTaskByID(kwt.KWTaskID);
                kwtask.Description = kwt.Description;
                kwtask.StaffProfile = kwt.StaffProfile;
                kwtask.Alert = kwt.Alert;

                int verify = kwtRepo.UpdateKWTask(kwtask);
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
