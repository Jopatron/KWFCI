using KWFCI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Repositories
{
    public interface IMessageRepository
    {
        IQueryable<Message> GetAllMessages();
        int AddMessage(Message message);

    }
}
