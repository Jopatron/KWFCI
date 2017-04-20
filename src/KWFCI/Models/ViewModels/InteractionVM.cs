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
    }
}
