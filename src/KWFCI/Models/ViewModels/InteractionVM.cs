using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Models.ViewModels
{
    public class InteractionVM
    {
        public List<Interaction> Interactions { get; set; }
        public Interaction NewInteraction { get; set; }
        public Broker Broker { get; set; }
        public int BrokerID { get; set; }
        public string Field { get; set; }
        public KWTask Task { get; set; }
        public List<KWTask> Tasks { get; set; }
        public List<Broker> AllBrokers { get; set; }
        public Decimal TasksCompleted { get; set; }
        public List<StaffProfile>AllStaff { get; set; }

    }
}
