using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KWFCI.Repositories;

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
            var allMessages = messageRepo.GetAllMessages().ToList();
            return View(allMessages);
        }
    }
}
