using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using KWFCI.Repositories;
using KWFCI.Models;

namespace KWFCI.Controllers
{
    public class InteractionsController : Controller
    {
        private IInteractionsRepository intRepo;

        public InteractionsController(IInteractionsRepository repo)
        {
            intRepo = repo;
        }

        public IActionResult BrokerInteractions()
        {
            //TODO Ensure user is rerouted if not logged in
            return View(intRepo.GetAllInteractions().ToList());
        }
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
        [HttpPost]
        public IActionResult AddInteraction(Interaction i)
        {
            Interaction interaction = new Interaction
            {
                Notes = i.Notes,
                NextStep = i.NextStep
            };

            intRepo.AddInteraction(i);
            //TODO: See if there is a way to just close the modal and not refresh the page
            return RedirectToAction("Index", "Home");

        }
        //[Route("Edit")]
        //public ActionResult Edit(int id)
        //{
        //    Interaction interaction = intRepo.GetInteractionById(id);
        //    if (interaction != null)
        //    {
        //        return View(interaction);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        [Route("Edit")]
        [HttpPost]
        public IActionResult Edit(Interaction i)
        {
            if (i != null)
            {
                Interaction interaction = intRepo.GetInteractionById(i.InteractionID);
                interaction.DateCreated = i.DateCreated;
                interaction.NextStep = i.NextStep;
                interaction.Notes = i.Notes;
                interaction.Status = i.Status;
                interaction.Broker = i.Broker;

                int verify = intRepo.UpdateInteraction(i);
                if (verify == 1)
                {
                    //TODO add feedback of success
                    return RedirectToAction("Index");
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
            return View(i);
        }
    }
}
