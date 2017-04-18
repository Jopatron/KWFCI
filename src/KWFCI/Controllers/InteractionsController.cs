using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using KWFCI.Repositories;

namespace KWFCI.Controllers
{
    public class InteractionsController : Controller
    {
        private IInteractionsRepository intRepo;

        public InteractionsController(IInteractionsRepository repo)
        {
            intRepo = repo;
        }

        public IActionResult Index()
        {
            //TODO Ensure user is rerouted if not logged in
            return View("AllInteractions", intRepo.GetAllInteractions().ToList());
        }
    }
}
