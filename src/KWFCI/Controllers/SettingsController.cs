using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KWFCI.Models;
using KWFCI.Repositories;
using Microsoft.AspNetCore.Identity;

namespace KWFCI.Controllers
{
    [Authorize(Roles = "Staff")]
    public class SettingsController : Controller
    {
        private IStaffProfileRepository profileRepo;
        private UserManager<StaffUser> userManager;

        public SettingsController(IStaffProfileRepository repo, UserManager<StaffUser>usrMgr)
        {
            profileRepo = repo;
            userManager = usrMgr;
        }


        public IActionResult Index()
        {
            if (Helper.StaffProfileLoggedIn == null)
                return RedirectToAction("Login", "Auth");
            // TODO: Grab logged in user profile
            var me = Helper.DetermineProfile(profileRepo);
            // TODO: Send Staff Profile to View
            return View(me);
        }

        [HttpPost]
        public IActionResult Index(StaffProfile p)
        {
            if (p != null)
            {
                StaffProfile profile = profileRepo.GetStaffProfileByID(p.StaffProfileID);
                profile.Email = p.Email;
                profile.User.UserName = p.Email;
                profile.User.NormalizedUserName = p.Email.ToUpper();
                profile.FirstName = p.FirstName;
                profile.LastName = p.LastName;
                profile.EmailNotifications = p.EmailNotifications;

                int verify = profileRepo.UpdateStaff(profile);
                if (verify == 1)
                {
                    //TODO add feedback of success
                    return RedirectToAction("Index","Home");
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
            return View(p);
        }

    }
}
