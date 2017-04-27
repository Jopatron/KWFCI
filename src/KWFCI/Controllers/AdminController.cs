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
        private IStaffProfileRepository staffRepo;

        public AdminController(IInteractionsRepository repo, IStaffProfileRepository repo2)
        {
            intRepo = repo;
            staffRepo = repo2;
        }

        [Route("Interactions")]
        public IActionResult AdminInteractions()
        {
            var vm = new AdminInteractionVM();
            vm.Staff = staffRepo.GetAllStaffProfiles().ToList();
            //TODO Ensure user is rerouted if not logged in
            return View(vm);
        }
    }
}
