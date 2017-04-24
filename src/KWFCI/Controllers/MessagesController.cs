using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KWFCI.Repositories;
using KWFCI.Models.ViewModels;
using KWFCI.Models;

namespace KWFCI.Controllers
{
    [Authorize(Roles = "Staff")]
    [Route("Messages")]
    public class MessagesController : Controller
    {
        private IMessageRepository messageRepo;

        public MessagesController(IMessageRepository repo)
        {
            messageRepo = repo;
        }

        
        public ViewResult AllMessages()
        {
            var vm = new MessageVM();
            var allMessages = messageRepo.GetAllMessages().ToList();

            vm.Messages = allMessages;
            vm.NewMessage = new Message();
            return View(vm);
        }
        [Route("Add")]
        [HttpPost]
        public IActionResult SendMessage(Message m, bool allBrokers = false)
        {

            var message = new Message
            {
                Subject = m.Subject,
                Body = m.Body,
                DateSent = DateTime.Now,
                
        };

            messageRepo.AddMessage(message);
             //TODO: See if there is a way to just close the modal and not refresh the page
            return RedirectToAction("AllMessages", "Messages");
        }
    }
}
