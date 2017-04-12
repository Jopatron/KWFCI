using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Models
{
    public class Broker : Person
    {
        public int BrokerID { get; set; }
        public List<Interaction> Interactions { get; set; }
        public List<KWTask> Requirements { get; set; }
        public string Type { get; set; }

        //TODO Will probably have to generate the list of requirements in the constructor
    }
}
