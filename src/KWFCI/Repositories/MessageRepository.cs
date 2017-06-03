using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KWFCI.Models;
using Microsoft.EntityFrameworkCore;

namespace KWFCI.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private ApplicationDbContext context;

        public MessageRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public int AddMessage(Message message)
        {
            message.From = Helper.StaffProfileLoggedIn.Email;
            context.Messages.Add(message);
            return context.SaveChanges(); 
        }

        public IQueryable<Message> GetAllMessages()
        {
            return context.Messages.AsQueryable();
        }
    }
}
