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

        public int ChangeStatus(Broker broker, string status)
        {
            broker.Status = status;
            int updateSuccess = UpdateBroker(broker);
            if(updateSuccess == 1)
            {
                return 1;
            }
            else
                return 0;
            
        }

        public int DeleteBroker(Broker broker)
        {
            context.Brokers.Remove(broker);
            return context.SaveChanges();
        }

        public IQueryable<Broker> GetAllBrokers(bool getInactive = false, bool getNotifications = false)
        {
            if (getInactive == true)
                return context.Brokers.Include(b => b.Interactions).Include(b => b.Requirements).AsQueryable();
            else if (getNotifications == true)
            {
                return (from b in context.Brokers
                        where b.EmailNotifications == true
                        select b).Include(b => b.Interactions).Include(b => b.Requirements).AsQueryable();
            }
            else
                return context.Brokers.Where(b => b.Status == "Active").Include(b => b.Interactions).Include(b => b.Requirements).AsQueryable();
        }

        public Broker GetBrokerByID(int id)
        {
            return (from b in context.Brokers
                    where b.BrokerID == id
                    select b).Include(b => b.Interactions).Include(b => b.Requirements).FirstOrDefault<Broker>();
        }

        public Broker GetBrokerByEmail(string email)
        {
            return (from b in context.Brokers
                    where b.Email == email
                    select b).Include(b => b.Interactions).Include(b => b.Requirements).FirstOrDefault<Broker>();
        }

        public IQueryable<Broker> GetBrokersByType(string type, bool getNotifications = false)
        {
            if (getNotifications == true)
            {
                var brokers = (from bro in context.Brokers
                               where bro.EmailNotifications == true
                               select bro).Include(b => b.Interactions).Include(b => b.Requirements).AsQueryable();

                return (from b in brokers
                        where b.Type == type
                        select b).Include(b => b.Interactions).Include(b => b.Requirements).AsQueryable();
            }
            else
            {
                return (from b in context.Brokers
                        where b.Type == type
                        select b).Include(b => b.Interactions).Include(b => b.Requirements).AsQueryable();
            }
        }

        public int UpdateBroker(Broker broker)
        {
            context.Brokers.Update(broker);
            return context.SaveChanges();
        }
    }
}
