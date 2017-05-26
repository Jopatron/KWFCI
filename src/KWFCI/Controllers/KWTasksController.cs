using KWFCI.Models;
using KWFCI.Models.ViewModels;
using KWFCI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Controllers
{
    [Authorize(Roles = "Staff")]
    [Route("Tasks")]
    public class KWTasksController : Controller
    {
        private IKWTaskRepository taskRepo;
        private IStaffProfileRepository staffRepo;
        private IInteractionsRepository intRepo;
        private readonly ApplicationDbContext _context;

        public KWTasksController(IKWTaskRepository repo, IStaffProfileRepository repo2, IInteractionsRepository repo3, ApplicationDbContext context)
        {
            taskRepo = repo;
            staffRepo = repo2;
            intRepo = repo3;
            _context = context;
        }
        //[Route("Index")]
        public IActionResult AllKWTasks(string filter)
        {
            var criticalVB = taskRepo.GetAllTasksByType("Alert").Where(t => t.Priority == 5).ToList();

            foreach (KWTask task in taskRepo.GetAllTasksByType("Task").Where(t => t.Priority == 5).ToList())
            {
                criticalVB.Add(task);
            }

            ViewBag.Critical = criticalVB;

            ViewBag.Filter = filter;
            var vm = new KWTaskVM();
            vm.StaffList = staffRepo.GetAllStaffProfiles().ToList();
            var KWTasks = taskRepo.GetAllKWTasks().Where(t => t.Type != "Onboarding").ToList();

            foreach(KWTask t in KWTasks.ToList())
            {
                if(t.Priority == 5)
                {
                    StaffProfile profile = staffRepo.GetProfileByTask(t);
                    if(profile != null)
                    {
                        profile.Tasks.Remove(t);
                        staffRepo.UpdateStaff(profile);
                    }
                }
                foreach(StaffProfile s in vm.StaffList)
                {
                    if(s.Tasks.Contains(t))
                    {
                        t.StaffName = s.FirstName + " " + s.LastName;
                        t.StaffEmail = s.Email;
                    }
                }
            }
            vm.KWTasks = KWTasks;
            vm.NewKWTask = new KWTask();
            return View(vm);
        }

        [Route("Add")]
        [HttpPost]
        public IActionResult AddKWTask(KWTaskVM vm, int? staffProfileID, string returnURL)
        {
            var kwtask = new KWTask
            {
                Message = vm.NewKWTask.Message,
                AlertDate = vm.NewKWTask.AlertDate,
                DateCreated = vm.NewKWTask.DateCreated,
                DateDue = vm.NewKWTask.DateDue,
                Priority = vm.NewKWTask.Priority
            };
            if (kwtask.AlertDate == null)
                kwtask.Type = "Task";
            else
                kwtask.Type = "Alert";

            if(staffProfileID != null)
            {
                StaffProfile staff = staffRepo.GetStaffProfileByID((int)staffProfileID);
                staff.Tasks.Add(kwtask);
            }

            taskRepo.AddKWTask(kwtask);

            //TODO: See if there is a way to just close the modal and not refresh the page
            return Redirect(returnURL);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            KWTask kwtask = taskRepo.GetKWTaskByID(id);
            if (kwtask != null)
            {
                var interaction = taskRepo.GetAssociatedInteraction(kwtask);
                if (interaction != null)
                {
                    interaction.Task = null;
                    interaction.TaskForeignKey = null;
                    intRepo.UpdateInteraction(interaction);
                }
                    
                taskRepo.DeleteKWTask(kwtask);
                return RedirectToAction("AllKWTasks");
            }
            else
            {
                ModelState.AddModelError("", "Task Not Found");
            }
            return RedirectToAction("AllKWTasks");
        }

        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(KWTaskVM vm, byte[] rowVersion, int KWTaskID)
        {
            var kwtaskToUpdate = await _context.KWTasks.Include(t => t.Interaction).SingleOrDefaultAsync(t => t.KWTaskID == vm.NewKWTask.KWTaskID);

            if (kwtaskToUpdate == null)
            {
                //Broker deletedBroker = new Broker();
                //await TryUpdateModelAsync(deletedBroker);
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The broker was deleted by another user.");
                ViewBag.ModelState = ModelState;
                return View("AllBrokers");
            }

            _context.Entry(kwtaskToUpdate).Property("RowVersion").OriginalValue = rowVersion;

            if (await TryUpdateModelAsync<KWTask>(
                kwtaskToUpdate,
                "",
                t => t.Message,
                t => t.Priority,
                t => t.Type,
                t => t.AlertDate,
                t => t.DateCreated,
                t => t.DateDue,
                t => t.StaffEmail,
                t => t.StaffName))
                
            {
                try
                {
                    if (kwtaskToUpdate.AlertDate == null)
                        kwtaskToUpdate.Type = "Task";
                    else
                        kwtaskToUpdate.Type = "Alert";
                    await _context.SaveChangesAsync();
                    return RedirectToAction("AllKWTasks");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (KWTask)exceptionEntry.Entity;
                    var databaseEntry = _context.KWTasks.AsNoTracking().Single(task => task.KWTaskID == ((KWTask)exceptionEntry.Entity).KWTaskID);
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The Task was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = databaseEntry;

                        if (databaseValues.Message != clientValues.Message)
                        {
                            ModelState.AddModelError("Message", $"Unable to save User entered value for Message: {clientValues.Message}");
                            ModelState.AddModelError("Message", $"Message Current Value: {databaseValues.Message}");
                        }
                        if (databaseValues.Priority != clientValues.Priority)
                        {
                            ModelState.AddModelError("Priority", $"Unable to save User entered value for Priority: { clientValues.Priority}");
                            ModelState.AddModelError("Priority", $"Priority Current value: {databaseValues.Priority}");
                        }
                        if (databaseValues.Type != clientValues.Type)
                        {
                            ModelState.AddModelError("Type", $"Unable to save User entered value for Type: {clientValues.Type}");
                            ModelState.AddModelError("Type", $"Type Current value: {databaseValues.Type}");
                        }
                        if (databaseValues.AlertDate != clientValues.AlertDate)
                        {
                            ModelState.AddModelError("AlertDate", $"Unable to save User entered value for AlertDate: { clientValues.AlertDate}");
                            ModelState.AddModelError("AlertDate", $"AlertDate Current value: {databaseValues.AlertDate}");
                        }
                        if (databaseValues.DateDue != clientValues.DateDue)
                        {
                            ModelState.AddModelError("DateDue", $"Unable to save User entered value for DateDue: { clientValues.DateDue}");
                            ModelState.AddModelError("DateDue", $"DateDue Current value: {databaseValues.DateDue}");
                        }
                        if (databaseValues.StaffEmail != clientValues.StaffEmail)
                        {
                            ModelState.AddModelError("StaffEmail", $"Unable to save User entered value for StaffEmail: { clientValues.StaffEmail}");
                            ModelState.AddModelError("StaffEmail", $"StaffEmail Current value: {databaseValues.StaffEmail}");
                        }
                        if (databaseValues.StaffName != clientValues.StaffName)
                        {
                            ModelState.AddModelError("StaffName", $"Unable to save User entered value for StaffName: { clientValues.StaffName}");
                            ModelState.AddModelError("StaffName", $"StaffName Current value: {databaseValues.StaffName}");
                        }

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you got the original value. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to edit this record, open "
                                + "the Edit window again and re-enter your changes.");
                        kwtaskToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");

                    }
                }
            }

            List<string> errorMessages = new List<string>();

            var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

            foreach (var layer1 in errors)
            {
                foreach (var layer2 in layer1)
                {
                    errorMessages.Add(layer2.ErrorMessage);
                }
            }

            TempData["ErrorMessages"] = errorMessages;

            return Redirect(returnURL);
        }

        //    //KWTask kwtask = taskRepo.GetKWTaskByID(KWTaskID);
        //    //kwtask.Message = vm.NewKWTask.Message;
        //    //kwtask.AlertDate = vm.NewKWTask.AlertDate;
        //    //kwtask.DateDue = vm.NewKWTask.DateDue;
        //    //kwtask.Priority = vm.NewKWTask.Priority;
        //    //kwtask.Type = vm.NewKWTask.Type;

        //    //if (kwtask.AlertDate == null)
        //    //    kwtask.Type = "Task";
        //    //else
        //    //    kwtask.Type = "Alert";

        //    //int verify = taskRepo.UpdateKWTask(kwtask);
        //    //if (verify == 1)
        //    //{
        //    //    return RedirectToAction("AllKWTasks");
        //    //}
        //    //else
        //    //{
        //    //    ModelState.AddModelError("", "Task Not Found");
        //    //}
        //    //return RedirectToAction("AllKWTasks");
    }

        [Route("Assign")]
        [HttpPost]
        public ActionResult Assign(int KWTaskID, string StaffProfileName)
        {
            bool verify;
            var task = taskRepo.GetKWTaskByID(KWTaskID);
            var allProfiles = staffRepo.GetAllStaffProfiles().ToList();
            if (StaffProfileName != "Clear")
            {
                string[] name = StaffProfileName.Split(' ');
                var profile = staffRepo.GetStaffProfileByFullName(name[0], name[1]) as StaffProfile;
                verify = ProcessAssign(task, allProfiles, profile);

                if (verify)
                {
                    return RedirectToAction("AllKWTasks");
                }
                else
                {
                    ModelState.AddModelError("", "Task Not Found");
                }
            }
            else
            {
                verify = ProcessAssign(task, allProfiles, true);
            }
            
            return RedirectToAction("AllKWTasks");
        }


        //First unassigns the task from staff, then assigns it, then updates the repo
        private bool ProcessAssign(KWTask t, List<StaffProfile> sps, StaffProfile staff)
        {
            foreach(StaffProfile sp in sps)
            {
                if (sp.Tasks.Contains(t))
                    sp.Tasks.Remove(t);
            }
            staff.Tasks.Add(t);
            int verify = staffRepo.UpdateStaff(staff);
            if (verify > 0)
                return true;
            else
                return false;
        }

        private bool ProcessAssign(KWTask t, List<StaffProfile> sps, bool clear)
        {
            foreach (StaffProfile sp in sps)
            {
                if (sp.Tasks.Contains(t))
                {
                    sp.Tasks.Remove(t);
                    staffRepo.UpdateStaff(sp);
                }
            }
            return true;
        }
    }
}
