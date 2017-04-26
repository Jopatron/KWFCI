using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KWFCI.Repositories;
using KWFCI.Models.ViewModels;

namespace KWFCI.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private IInteractionsRepository intRepo;

        public AdminController(IInteractionsRepository repo)
        {
            intRepo = repo;
        }

        [Route("Interactions")]
        public IActionResult AdminInteractions()
        {
            var vm = new AdminInteractionVM();
            vm.Interactions = intRepo.GetAllInteractions().ToList();
            //TODO Ensure user is rerouted if not logged in
            return View(vm);
        }
    }
}
