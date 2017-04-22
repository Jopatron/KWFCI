using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KWFCI.Models;
using KWFCI.Repositories;

namespace KWFCI.Tests.FakeRepositories
{
    public class FakeMessageRepository : IMessageRepository
    {
        public List<Message> messages = new List<Message>();

        public FakeMessageRepository()
        {
            Message message = new Message
            {
                Subject = "Test Subject",
                Body = "This is the body of the email message",
                DateSent = DateTime.Now,
                MessageID = 1

            };
            messages.Add(message);

            Message message2 = new Message
            {
                Subject = "Another Subject",
                Body = "This is the body of the second email message",
                DateSent = DateTime.Now,
                MessageID = 2

            };
            messages.Add(message2);


        }
        public int AddMessage(Message message)
        {
            messages.Add(message);
            if (messages.Contains(message))
                return 1;
            else
                return 0;
        }

        public IQueryable<Message> GetAllMessages()
        {
            return messages.AsQueryable();
        }
    }
}
