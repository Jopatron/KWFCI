using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace KWFCI.Controllers
{
    public class InteractionsController : Controller
    {
        public IActionResult Index()
        {
            //TODO Ensure user is rerouted if not logged in
            return View("AllInteractions");
        }
    }
}
