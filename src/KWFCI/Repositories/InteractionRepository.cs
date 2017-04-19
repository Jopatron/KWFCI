using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KWFCI.Models;
using Microsoft.EntityFrameworkCore;

namespace KWFCI.Repositories
{
    public class InteractionRepository : IInteractionsRepository
    {
        private ApplicationDbContext context;

        public InteractionRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public int AddInteraction(Interaction interaction)
        {
            context.Interactions.Add(interaction);
            return context.SaveChanges();
        }

        public int DeleteInteraction(Interaction interaction)
        {
            context.Interactions.Remove(interaction);
            return context.SaveChanges();
        }

        public IQueryable<Interaction> GetAllInteractions()
        {
            return context.Interactions.Where(i => i.Status == "Active").AsQueryable();
        }

        public Interaction GetInteractionById(int id)
        {
            return (from i in context.Interactions
                    where i.InteractionID == id
                    select i).FirstOrDefault<Interaction>();
        }

        //public IQueryable<Interaction> GetInteractionsByBroker(Broker broker)
        //{
        //    return (from i in context.Interactions
        //            where i.Broker.BrokerID == broker.BrokerID
        //            select i);
        //}
        public int ChangeStatus(Interaction interaction, string status)
        {
            interaction.Status = status;
            int updateSuccess = UpdateInteraction(interaction);
            if (updateSuccess == 1)
            {
                return 1;
            }
            else
                return 0;
        }

        //public IQueryable<Interaction> GetInteractionsByStaff(StaffProfile stafProf)
        //{
        //    return (from i in context.Interactions
        //            where i.StaffProfile.StaffProfileID == stafProf.StaffProfileID
        //            select i);
        //}

        public int UpdateInteraction(Interaction interaction)
        {
            context.Interactions.Update(interaction);
            return context.SaveChanges();
        }

        
    }
}
