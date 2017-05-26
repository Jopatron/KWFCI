using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KWFCI.Repositories;
using KWFCI.Models;
using KWFCI.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace KWFCI.Controllers
{
    [Authorize(Roles = "Staff")]
    [Route("Brokers")]
    public class BrokersController : Controller
    {
        private IBrokerRepository brokerRepo;
        private IKWTaskRepository taskRepo;
        private readonly ApplicationDbContext _context;

        public BrokersController(IBrokerRepository repo, IKWTaskRepository repo2, ApplicationDbContext context)
        {
            brokerRepo = repo;
            taskRepo = repo2;
            _context = context;
        }
        
        public ViewResult AllBrokers()
        {
            if (TempData["ErrorMessages"] != null)
            {
                ViewBag.Errors = TempData["ErrorMessages"];
            }

            //ViewBag.ModelState = TempData["ModelState"];
            ViewBag.Critical = taskRepo.GetAllTasksByType("Alert").Where(t => t.Priority == 5).ToList();
            var vm = new BrokerVM();
            vm.Brokers = brokerRepo.GetAllBrokers().ToList();
            vm.NewBroker = new Broker();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Archive(int id)
        {
            Broker broker = brokerRepo.GetBrokerByID(id);
            if (broker != null)
            {
                brokerRepo.ChangeStatus(broker, "Inactive");
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return RedirectToAction("Index", brokerRepo.GetAllBrokers().ToList());
        }
        [Route("Add")]
        [HttpPost]
        public IActionResult AddBroker(Broker b, string returnURL)
        {
            var broker = new Broker {
                FirstName = b.FirstName,
                LastName = b.LastName,
                Email = b.Email,
                Type = b.Type,
                EmailNotifications = b.EmailNotifications,
                Requirements = new List<KWTask>()
                
            };

            if (broker.Type == "New Broker")
            {
                var requirements = broker.CreateRequirementsList();

                foreach (KWTask r in requirements)
                    broker.Requirements.Add(r);
            }
            

            brokerRepo.AddBroker(broker);
            //TODO: See if there is a way to just close the modal and not refresh the page
            return Redirect(returnURL);
        }
        

        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(Broker b, byte[] rowVersion, string returnURL)
        {
            var brokerToUpdate = await _context.Brokers.Include(i => i.Interactions).Include(i => i.Requirements).SingleOrDefaultAsync(i => i.BrokerID == b.BrokerID);

            if (brokerToUpdate == null)
            {
                //Broker deletedBroker = new Broker();
                //await TryUpdateModelAsync(deletedBroker);
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The broker was deleted by another user.");
                ViewBag.ModelState = ModelState;
                return View("AllBrokers");
            }

            _context.Entry(brokerToUpdate).Property("RowVersion").OriginalValue = rowVersion;

            if (await TryUpdateModelAsync<Broker>(
                brokerToUpdate, 
                "", 
                br => br.Email, 
                br => br.FirstName, 
                br => br.LastName, 
                br => br.Status,
                br => br.Type))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return Redirect(returnURL);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Broker)exceptionEntry.Entity;
                    var databaseEntry = _context.Brokers.AsNoTracking().Single(bro => bro.BrokerID == ((Broker)exceptionEntry.Entity).BrokerID);
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The Broker was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = databaseEntry;

                        if (databaseValues.FirstName != clientValues.FirstName)
                        {
                            ModelState.AddModelError("FirstName", $"Unable to save User entered value for FirstName: {clientValues.FirstName}");
                            ModelState.AddModelError("FirstName", $"FirstName Current Value: {databaseValues.FirstName}");
                        }
                        if (databaseValues.LastName != clientValues.LastName)
                        {
                            ModelState.AddModelError("LastName", $"Unable to save User entered value for LastName: { clientValues.LastName}");
                            ModelState.AddModelError("LastName", $"LastName Current value: {databaseValues.LastName}");
                        }
                        if (databaseValues.Type != clientValues.Type)
                        {
                            ModelState.AddModelError("Type", $"Unable to save User entered value for Type: {clientValues.Type}");
                            ModelState.AddModelError("Type", $"Type Current value: {databaseValues.Type}");
                        }
                        if (databaseValues.Email != clientValues.Email)
                        {
                            ModelState.AddModelError("Email", $"Unable to save User entered value for Email: { clientValues.Email}");
                            ModelState.AddModelError("Email", $"Email Current value: {databaseValues.Email}");
                        }
                        if (databaseValues.Status != clientValues.Status)
                        {
                            ModelState.AddModelError("Status", $"Unable to save User entered value for Status: { clientValues.Status}");
                            ModelState.AddModelError("Status", $"Status Current value: {databaseValues.Status}");
                        }

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you got the original value. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to edit this record, open "
                                + "the Edit window again and re-enter your changes.");
                        brokerToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");

                    }
                }
            }

            List<string> errorMessages = new List<string>();

            var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
            
            foreach(var layer1 in errors)
            {
                foreach(var layer2 in layer1)
                {
                    errorMessages.Add(layer2.ErrorMessage);
                }
            }

            TempData["ErrorMessages"] = errorMessages;

            return Redirect(returnURL);
        }
    }
}
