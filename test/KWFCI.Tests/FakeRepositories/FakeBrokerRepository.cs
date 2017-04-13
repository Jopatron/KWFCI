using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KWFCI.Models;
using KWFCI.Repositories;

namespace KWFCI.Tests.FakeRepositories
{
    public class FakeBrokerRepository : IBrokerRepository
    {
        public List<Broker> brokers = new List<Broker>();

        public FakeBrokerRepository()
        {
            Broker broker = new Broker { FirstName = "Lonny", LastName = "Jenkins", Email = "ljenkins@kw.com", EmailNotifications = true, Type = "New Broker" };
            brokers.Add(broker);

            broker = new Broker { FirstName = "Samantha", LastName = "Coldwater", Email = "scoldwater@kw.com", EmailNotifications = true, Type = "In Transition" };
            brokers.Add(broker);

            broker = new Broker { FirstName = "Brooke", LastName = "Schelling", Email = "bschelling@kw.com", EmailNotifications = true, Type = "New Broker" };
            brokers.Add(broker);
        }



        public int AddBroker(Broker broker)
        {
            brokers.Add(broker);
            if (brokers.Contains(broker))
                return 1;
            else
                return 0;
        }

        public int DeleteBroker(Broker broker)
        {
            brokers.Remove(broker);
            if (!brokers.Contains(broker))
                return 1;
            else
                return 0;
        }

        public IQueryable<Broker> GetAllBrokers()
        {
            return brokers.AsQueryable();
        }

        public Broker GetBrokerByID(int id)
        {
            return brokers.Where(b => b.BrokerID == id) as Broker;
        }

        public IQueryable<Broker> GetBrokersByType(string type)
        {
            return brokers.Where(b => b.Type == type).AsQueryable();
        }

        public int UpdateBroker(Broker newBroker)
        {
            Broker oldBroker = brokers.Where(b => b.BrokerID == newBroker.BrokerID) as Broker;
            oldBroker.Type = newBroker.Type;
            oldBroker.FirstName = newBroker.FirstName;
            oldBroker.LastName = newBroker.LastName;
            oldBroker.Email = newBroker.Email;
            oldBroker.EmailNotifications = newBroker.EmailNotifications;

            int index = brokers.IndexOf(oldBroker);
            brokers.Remove(oldBroker);
            brokers.Insert(index, oldBroker);

            if (brokers.IndexOf(oldBroker) == index)
                return 1;
            else
                return 0;
        }
    }
}
