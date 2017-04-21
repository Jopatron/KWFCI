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

        public InteractionsController(IInteractionsRepository repo, IBrokerRepository repo2, IStaffProfileRepository repo3)
        {
            intRepo = repo;
            brokerRepo = repo2;
            staffRepo = repo3;
        }
        //[Route("Test")]
        //public ActionResult TestBroker()
        //{
        //    ViewBag.Email = Helper.StaffProfileLoggedIn.Email;
        //    Broker broker = brokerRepo.GetAllBrokers().First();
        //    return View(broker);
        //}
        [Route("Brokers")]
        public IActionResult BrokerInteractions(int BrokerID)
        {
            var broker = brokerRepo.GetBrokerByID(BrokerID);
            ViewBag.BrokerName = broker.FirstName + " " + broker.LastName;
            ViewBag.StaffEmail = Helper.StaffProfileLoggedIn.Email;
            var allInteractions = broker.Interactions;
            var vm = new InteractionVM();
            vm.Interactions = allInteractions;
            vm.Broker = broker;
            vm.NewInteraction = new Interaction();
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
        public ActionResult Edit(InteractionVM iVM)
        {
            var i = iVM.NewInteraction;
            if (i != null)
            {
                Interaction interaction = intRepo.GetInteractionById(i.InteractionID);

                if(iVM.Field == "Notes")
                    interaction.Notes = i.Notes;
                else if(iVM.Field == "NextStep")
                    interaction.NextStep = i.NextStep;
                //interaction.DateCreated = i.DateCreated;
                //interaction.Status = i.Status;

                int verify = intRepo.UpdateInteraction(interaction);
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
