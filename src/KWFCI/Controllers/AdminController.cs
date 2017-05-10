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
        private IBrokerRepository brokerRepo;
        private IKWTaskRepository taskRepo;

        public AdminController(IInteractionsRepository repo, IStaffProfileRepository repo2, UserManager<StaffUser> usrMgr, IBrokerRepository repo3, IKWTaskRepository repo4)
        {
            intRepo = repo;
            staffRepo = repo2;
            userManager = usrMgr;
            brokerRepo = repo3;
            taskRepo = repo4;
        }

        [Route("Home")]
        public IActionResult AdminHome(string Page)
        {
            ViewBag.Critical = taskRepo.GetAllTasksByType("Alert").Where(t => t.Priority == 5).ToList();
            ViewBag.Page = Page;
            return View();
        }

        [Route("Staff")]
        public IActionResult AdminStaff()
        {
            ViewBag.Page = "Staff";
            var vm = new AdminVM();
            vm.Staff = staffRepo.GetAllStaffProfiles().ToList();
            //TODO Ensure user is rerouted if not logged in
            return View("AdminHome", vm);
        }

        [Route("Interactions")]
        public IActionResult AdminInteractions()
        {
            ViewBag.Page = "Interactions";
            var vm = new AdminVM();
            vm.Staff = staffRepo.GetAllStaffProfiles().ToList();
            //TODO Ensure user is rerouted if not logged in
            return View("AdminHome", vm);
        }

        [Route("Brokers")]
        public IActionResult AdminBrokers()
        {
            ViewBag.Page = "Brokers";
            var vm = new AdminVM();
            vm.Brokers = brokerRepo.GetAllBrokers(true, false).ToList();
            //TODO Ensure user is rerouted if not logged in
            return View("AdminHome", vm);
        }

        [HttpPost]
        [Route("InteractionDelete")]
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
        [Route("StaffDelete")]
        public IActionResult StaffDelete(int id)
        {
            StaffProfile profile = staffRepo.GetStaffProfileByID(id);
            if (profile != null)
            {
                staffRepo.DeleteStaff(profile);
                return RedirectToAction("Interactions");
            }
            else
            {
                ModelState.AddModelError("", "Staff Not Found");
            }
            return RedirectToAction("Home");
        }

        [HttpPost]
        [Route("BrokerDelete")]
        public IActionResult BrokerDelete(int id)
        {
            Broker broker = brokerRepo.GetBrokerByID(id);
            if (broker != null)
            {
                brokerRepo.DeleteBroker(broker);
                return RedirectToAction("Brokers");
            }
            else
            {
                ModelState.AddModelError("", "Broker Not Found");
            }
            return RedirectToAction("Home");
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddStaff(AdminVM vm)
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
