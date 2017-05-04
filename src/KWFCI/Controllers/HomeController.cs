using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KWFCI.Repositories;

namespace KWFCI.Controllers
{
    
    [Route("Index")]
    [Route("/")]
    [Authorize(Roles = "Staff")]
    public class HomeController : Controller
    {
        

        
        public IActionResult Index()
        {
            //TODO Ensure user is rerouted if not logged in
            return View();
        }
    }
}
