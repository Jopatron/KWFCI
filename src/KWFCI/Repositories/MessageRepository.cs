using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KWFCI.Models;

namespace KWFCI.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private ApplicationDbContext context;

        public MessageRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Message> GetAllMessages()
        {
            return context.Messages.AsQueryable();
        }
    }
}
