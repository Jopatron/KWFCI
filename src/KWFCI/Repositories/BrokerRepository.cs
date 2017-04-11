using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KWFCI.Models;
using Microsoft.EntityFrameworkCore;

//TODO Finish Broker repository
namespace KWFCI.Repositories
{
    public class BrokerRepository : IBrokerRepository
    {
        private ApplicationDbContext context;

        public BrokerRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public int AddBroker(Broker broker)
        {
            context.Brokers.Add(broker);
            return context.SaveChanges();
        }

        public int DeleteBroker(Broker broker)
        {
            context.Brokers.Remove(broker);
            return context.SaveChanges();
        }

        public IQueryable<Broker> GetAllBrokers()
        {
            return context.Brokers.Include(b => b.Interactions).Include(b => b.Requirements).AsQueryable();
        }

        public IQueryable<Broker> GetBrokersByType(string type)
        {
            return (from b in context.Brokers
                    where b.Type == type
                    select b).AsQueryable();
        }

        public int UpdateBroker(Broker broker)
        {
            context.Brokers.Update(broker);
            return context.SaveChanges();
        }
    }
}
