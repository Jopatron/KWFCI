using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KWFCI.Models;
using Microsoft.AspNetCore.Identity;
using KWFCI.Repositories;

namespace KWFCI.Controllers
{
    
    [Route("Index")]
    [Route("/")]
    [Authorize(Roles = "Staff")]
    public class HomeController : Controller
    {
        private UserManager<StaffUser> userManager;
        private IStaffProfileRepository staffProfRepo;

        public HomeController(UserManager<StaffUser> usrMgr, IStaffProfileRepository repo)
        {
            staffProfRepo = repo;
            userManager = usrMgr;
        }
        
        public async Task <IActionResult> Index()
        {
            StaffUser user = await userManager.FindByNameAsync(User.Identity.Name);
            Helper.StaffUserLoggedIn = user;
            Helper.StaffProfileLoggedIn = Helper.DetermineProfile(staffProfRepo);
            //TODO Ensure user is rerouted if not logged in
            return View();
        }
    }
}
