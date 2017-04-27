using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KWFCI.Repositories;
using KWFCI.Models.ViewModels;
using KWFCI.Models;
using Microsoft.AspNetCore.Identity;

namespace KWFCI.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private IInteractionsRepository intRepo;
        private IStaffProfileRepository staffRepo;
        private UserManager<StaffUser> userManager;

        public AdminController(IInteractionsRepository repo, IStaffProfileRepository repo2, UserManager<StaffUser> usrMgr)
        {
            intRepo = repo;
            staffRepo = repo2;
            userManager = usrMgr;
        }

        [Route("Home")]
        public IActionResult AdminHome()
        {
            return View();
        }

        [Route("Staff")]
        public IActionResult AdminStaff()
        {
            var vm = new AdminStaffVM();
            vm.Staff = staffRepo.GetAllStaffProfiles().ToList();
            //TODO Ensure user is rerouted if not logged in
            return View(vm);
        }

        [Route("Interactions")]
        public IActionResult AdminInteractions()
        {
            var vm = new AdminInteractionVM();
            vm.Staff = staffRepo.GetAllStaffProfiles().ToList();
            //TODO Ensure user is rerouted if not logged in
            return View(vm);
        }

        [HttpPost]
        public IActionResult InteractionDelete(int id)
        {
            Interaction i = intRepo.GetInteractionById(id);
            if (i != null)
            {
                intRepo.DeleteInteraction(i);
            }
            else
            {
                ModelState.AddModelError("", "Interaction Not Found");
            }
            return RedirectToAction("Interactions");
        }

        [HttpPost]
        public IActionResult StaffDelete(int id)
        {
            return RedirectToAction("Staff");
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddStaff(AdminStaffVM vm)
        {
            StaffUser user = new StaffUser { UserName = vm.NewStaff.Email };
            IdentityResult result = await userManager.CreateAsync(user, vm.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, vm.NewStaff.Role);
            };

            StaffProfile profile = new StaffProfile
            {
                FirstName = vm.NewStaff.FirstName,
                LastName = vm.NewStaff.LastName,
                Email = vm.NewStaff.Email,
                EmailNotifications = vm.NewStaff.EmailNotifications,
                User = user,
                Role = vm.NewStaff.Role
            };

            staffRepo.AddStaff(profile);

            return RedirectToAction("Home");
        }
    }
}
