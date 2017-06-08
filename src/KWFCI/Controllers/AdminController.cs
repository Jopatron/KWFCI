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
            /*Call the GetToday method of the helper class to get and set today's date for use with datepicker validation*/
            Helper.GetToday();

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
            vm.NewStaff = new StaffProfile();
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
            vm.NewBroker = new Broker();
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

        [Route("InteractionEdit")]
        [HttpPost]
        public IActionResult InteractionEdit(Interaction i)
        {
            if (i != null)
            {
                Interaction interaction = intRepo.GetInteractionById(i.InteractionID);
                interaction.Notes = i.Notes;
                interaction.DateCreated = i.DateCreated;
                interaction.NextStep = i.NextStep;
                interaction.Status = i.Status;

                int verify = intRepo.UpdateInteraction(interaction);
                if (verify == 1)
                {
                    //TODO add feedback of success
                    return RedirectToAction("Interactions");
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

        [HttpPost]
        [Route("StaffDelete")]
        public async Task <IActionResult> StaffDelete(int id)
        {
            StaffProfile profile = staffRepo.GetStaffProfileByID(id);
            if (profile != null)
            {
                StaffUser user = await userManager.FindByNameAsync(profile.Email);
               await userManager.DeleteAsync(user);
                staffRepo.DeleteStaff(profile);
                return RedirectToAction("Staff");
            }
            else
            {
                ModelState.AddModelError("", "Staff Not Found");
            }
            return RedirectToAction("Home");
        }

        [Route("StaffEdit")]
        [HttpPost]
        public IActionResult StaffEdit(StaffProfile sp)
        {
            if (sp != null)
            {
                StaffProfile staff = staffRepo.GetStaffProfileByID(sp.StaffProfileID);
                staff.Email = sp.Email;
                staff.FirstName = sp.FirstName;
                staff.LastName = sp.LastName;
                staff.Role = sp.Role;

                int verify = staffRepo.UpdateStaff(staff);
                if (verify == 1)
                {
                    //TODO add feedback of success
                    return RedirectToAction("Staff");
                }
                else
                {
                    //TODO add feedback for error
                }
            }
            else
            {
                ModelState.AddModelError("", "Staff Not Found");
            }
            return View(sp);
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

        [Route("BrokerEdit")]
        [HttpPost]
        public IActionResult BrokerEdit(Broker b)
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

                int verify = brokerRepo.UpdateBroker(broker);
                if (verify == 1)
                {
                    //TODO add feedback of success
                    return RedirectToAction("Brokers");
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

            return RedirectToAction("Staff");
        }
    }
}
