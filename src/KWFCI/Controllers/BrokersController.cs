using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KWFCI.Repositories;
using KWFCI.Models;
using KWFCI.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace KWFCI.Controllers
{
    [Authorize(Roles = "Staff")]
    [Route("Brokers")]
    public class BrokersController : Controller
    {
        private IBrokerRepository brokerRepo;
        private IKWTaskRepository taskRepo;

        public BrokersController(IBrokerRepository repo, IKWTaskRepository repo2)
        {
            brokerRepo = repo;
            taskRepo = repo2;
        }
        
        public ViewResult AllBrokers()
        {
            /*Call the GetToday method of the helper class to get and set today's date for use with datepicker validation*/
            Helper.GetToday();

            var criticalVB = taskRepo.GetAllTasksByType("Alert").Where(t => t.Priority == 5).ToList();

            foreach (KWTask task in taskRepo.GetAllTasksByType("Task").Where(t => t.Priority == 5).ToList())
            {
                criticalVB.Add(task);
            }

            ViewBag.Critical = criticalVB;
            var vm = new BrokerVM();
            vm.Brokers = brokerRepo.GetAllBrokers().ToList();
            vm.NewBroker = new Broker();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Archive(int id)
        {
            Broker broker = brokerRepo.GetBrokerByID(id);
            if (broker != null)
            {
                brokerRepo.ChangeStatus(broker, "Archived");
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return RedirectToAction("Index", brokerRepo.GetAllBrokers().ToList());
        }
        [Route("Add")]
        [HttpPost]
        public IActionResult AddBroker(Broker b, string returnURL)
        {
            var broker = new Broker {
                FirstName = b.FirstName,
                LastName = b.LastName,
                Email = b.Email,
                Type = b.Type,
                EmailNotifications = b.EmailNotifications,
                Requirements = new List<KWTask>()
                
            };

            if (broker.Type == "New Broker" || broker.Type == "Transfer Broker")
            {
                var requirements = broker.CreateRequirementsList();

                foreach (KWTask r in requirements)
                    broker.Requirements.Add(r);
            }
            

            brokerRepo.AddBroker(broker);
            //TODO: See if there is a way to just close the modal and not refresh the page
            return Redirect(returnURL);
        
        }
        //[Route("Edit")]
        //public ActionResult Edit(int id)
        //{
        //    Broker broker = brokerRepo.GetBrokerByID(id);
        //    if (broker != null)
        //    {
        //        return PartialView(broker);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        [Route("Edit")]
        [HttpPost]
        public IActionResult Edit(Broker b)
        {
            if (b != null)
            {
                Broker broker = brokerRepo.GetBrokerByID(b.BrokerID);
                broker.Email = b.Email;
                broker.FirstName = b.FirstName;
                broker.LastName = b.LastName;
                broker.Status = b.Status;
                broker.EmailNotifications = b.EmailNotifications;
                broker.Type = b.Type;
                //broker.UserName = member.UserName;

                int verify = brokerRepo.UpdateBroker(broker);
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
                ModelState.AddModelError("", "Broker Not Found");
            }
            return View(b);
        }
    }
}
