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
            //TODO: change this to add the logged in staff profile
            message.From = context.StaffProfiles.First();

            context.Messages.Add(message);
            return context.SaveChanges(); 
        }

        public IQueryable<Message> GetAllMessages()
        {
            return context.Messages.Include(p => p.From).AsQueryable();
        }
    }
}
