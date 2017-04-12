using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KWFCI.Repositories;
using KWFCI.Models;
using Microsoft.AspNetCore.Identity;

namespace KWFCI.Controllers
{
    //[Authorize(Roles = "Staff")]
    [Route("Brokers")]
    public class BrokersController : Controller
    {
        private IBrokerRepository brokerRepo;

        public BrokersController(IBrokerRepository repo)
        {
            brokerRepo = repo;
        }
        
        public IActionResult AllBrokers()
        {
            var allBrokers = brokerRepo.GetAllBrokers().ToList();
            return View(allBrokers);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Broker broker = brokerRepo.GetBrokerByID(id);
            if (broker != null)
            {
                brokerRepo.DeleteBroker(broker);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return RedirectToAction("Index", brokerRepo.GetAllBrokers().ToList());
        }

        public IActionResult AddBroker()
        {
            return View();
        }
        [Route("Edit")]
        public IActionResult Edit(int id)
        {
            Broker broker = brokerRepo.GetBrokerByID(id);
            if (broker != null)
            {
                return View(broker);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Route("Edit")]
        [HttpPost]
        public IActionResult Edit(Broker b)
        {
            Broker broker = brokerRepo.GetBrokerByID(b.BrokerID);
            if (broker != null)
            {
                broker.Email = b.Email;
                broker.FirstName = b.FirstName;
                broker.LastName = b.LastName;
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
                ModelState.AddModelError("", "User Not Found");
            }
            return View(broker);
        }
    }
}
