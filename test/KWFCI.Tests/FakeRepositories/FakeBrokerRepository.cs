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
            Broker broker = new Broker { FirstName = "Lonny", LastName = "Jenkins", Email = "ljenkins@kw.com", EmailNotifications = true, Type = "New Broker", BrokerID=1 };
            brokers.Add(broker);

            broker = new Broker { FirstName = "Samantha", LastName = "Coldwater", Email = "scoldwater@kw.com", EmailNotifications = true, Type = "In Transition", BrokerID = 2 };
            brokers.Add(broker);

            broker = new Broker { FirstName = "Brooke", LastName = "Schelling", Email = "bschelling@kw.com", EmailNotifications = true, Type = "New Broker", BrokerID = 3 };
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

        public int ChangeStatus(Broker broker, string status)
        {
            throw new NotImplementedException();
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

        public IQueryable<Broker> GetAllBrokers(bool getInactive = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Broker> GetAllBrokers(bool getInactive = false, bool getNotifications = false)
        {
            throw new NotImplementedException();
        }

        public Broker GetBrokerByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Broker GetBrokerByID(int id)
        {
            //var broker = brokers.Where(b => b.BrokerID == id) as Broker;
            var broker = (from b in brokers
                          where b.BrokerID == id
                          select b).FirstOrDefault();
            return broker;
        }

        public IQueryable<Broker> GetBrokersByType(string type)
        {
            return brokers.Where(b => b.Type == type).AsQueryable();
        }

        public IQueryable<Broker> GetBrokersByType(string type, bool getNotifications = false)
        {
            throw new NotImplementedException();
        }

        public int UpdateBroker(Broker broker)
        {
            int index = brokers.IndexOf(broker);
            //Broker b1 = brokers.Where(b => b.BrokerID == broker.BrokerID) as Broker;
            Broker b1 = (from b in brokers
                         where b.BrokerID == broker.BrokerID
                         select b).FirstOrDefault<Broker>();
            brokers.Remove(b1);

            b1.Type = broker.Type;
            b1.FirstName = broker.FirstName;
            b1.LastName = broker.LastName;
            b1.Email = broker.Email;
            b1.EmailNotifications = broker.EmailNotifications;
            
            brokers.Insert(index, b1);

            if (brokers.IndexOf(b1) == index)
                return 1;
            else
                return 0;
        }
    }
}
