using KWFCI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Repositories
{
    public interface IInteractionsRepository
    {
        IQueryable<Interaction> GetAllInteractions();
        //IQueryable<Interaction> GetInteractionsByStaff(StaffProfile stafProf);
        //IQueryable<Interaction> GetInteractionsByBroker(Broker broker);
        //int return value represents whether or not operation completed: 1 for True, 0 for False
        int DeleteInteraction(Interaction interaction);
        int AddInteraction(Interaction interaction);
        int UpdateInteraction(Interaction interaction);
        Interaction GetInteractionById(int id);
        int ChangeStatus(Interaction interaction, string status);
    }
}
