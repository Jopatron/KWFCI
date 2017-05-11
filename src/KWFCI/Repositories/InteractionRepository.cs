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
            return context.Interactions.Where(i => i.Status == "Active").Include(i => i.Task).AsQueryable();
        }

        public Interaction GetInteractionById(int id)
        {
            return (from i in context.Interactions
                    where i.InteractionID == id
                    select i).Include(i => i.Task).FirstOrDefault<Interaction>();
        }

        
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

        

        public int UpdateInteraction(Interaction interaction)
        {
            context.Interactions.Update(interaction);
            return context.SaveChanges();
        }

        public List<Interaction> GetAllInteractionsForStaff(int staffID)
        {
            return context.Interactions.FromSql("SELECT * FROM dbo.Interactions WHERE StaffProfileID =" + staffID).ToList();
        }

    }
}
