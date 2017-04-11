using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Models
{
    public class Broker : Person
    {
        public List<Interaction> Interactions { get; set; }
        public List<Task> Requirements { get; set; }
        public string Type { get; set; }
    }
}
