using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace KWFCI.Controllers
{
    //[Authorize(Roles="Staff")]
    [Route("Index")]
    [Route("/")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //TODO Ensure user is rerouted if not logged in
            return View();
        }
    }
}
