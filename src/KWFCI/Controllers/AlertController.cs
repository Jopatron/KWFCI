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
    [Route("Alerts")]
    public class AlertController : Controller
    {
        private IAlertRepository alertRepo;

        public AlertController(IAlertRepository repo)
        {
            alertRepo = repo;
        }

        public IActionResult AllAlerts()
        {
            var allAlerts = alertRepo.GetAllAlerts().ToList();
            var vm = new AlertVM();
            vm.Alerts = allAlerts;
            return View(vm);
        }

        [Route("Add")]
        [HttpPost]
        public IActionResult AddAlert(AlertVM vm)
        {
            var a = vm.AddedAlert;
            var alert = new Alert
            {
                Priority = a.Priority,
                AlertDate = a.AlertDate,
                Message = a.Message
            };

            alertRepo.AddAlert(alert);
            //TODO: See if there is a way to just close the modal and not refresh the page
            return RedirectToAction("AllAlerts");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Alert alert = alertRepo.GetAlertByID(id);
            if (alert != null)
            {
                alertRepo.DeleteAlert(alert);
                return RedirectToAction("AllAlerts");
            }
            else
            {
                ModelState.AddModelError("", "Alert Not Found");
            }
            return RedirectToAction("AllAlerts");
        }

        [Route("Edit")]
        public ActionResult Edit(int id)
        {
            Alert alert = alertRepo.GetAlertByID(id);
            if (alert != null)
            {
                return View(alert);
            }
            else
            {
                return RedirectToAction("AllAlerts");
            }
        }

        [Route("Edit")]
        [HttpPost]
        public IActionResult Edit(Alert a)
        {
            if (a != null)
            {
                Alert alert = alertRepo.GetAlertByID(a.AlertID);
                alert.AlertDate = a.AlertDate;
                alert.Priority = a.Priority;
                alert.Message = a.Message;

                int verify = alertRepo.UpdateAlert(alert);
                if (verify == 1)
                {
                    //TODO add feedback of success
                    return RedirectToAction("AllAlerts");
                }
                else
                {
                    //TODO add feedback for error
                }
            }
            else
            {
                ModelState.AddModelError("", "Alert Not Found");
            }
            return View(a);
        }
    }
}
