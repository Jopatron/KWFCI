using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace KWFCI.Controllers
{
    [Authorize(Roles = "Staff")]
    public class SettingsController : Controller
    {
        
        public IActionResult Index()
        {
            if (Helper.StaffProfileLoggedIn == null)
                return RedirectToAction("Login", "Auth");
            // TODO: Grab logged in user profile
            var profile = Helper.StaffProfileLoggedIn;
            // TODO: Send Staff Profile to View
            return View(profile);
        }
    }
}
