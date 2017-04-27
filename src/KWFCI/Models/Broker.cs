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
        public string Type { get; set; } //New Broker, In Transition, or Transfer
        public string Status { get; set; } //Active or Inactive

        public Broker()
        {
            Interactions = new List<Interaction>();
            Status = "Active";
            if (Type == "New Broker")
                Requirements = CreateRequirementsList();
        }

        public List<KWTask> CreateRequirementsList()
        {
            return new List<KWTask>() {
                new KWTask() { Message = "Requirement 1"},
                new KWTask() { Message = "Requirement 2"},
                new KWTask() { Message = "Requirement 3"},
                new KWTask() { Message = "Requirement 4"},
                new KWTask() { Message = "Requirement 5"},
                new KWTask() { Message = "Requirement 6"},
                new KWTask() { Message = "Requirement 7"},
                new KWTask() { Message = "Requirement 8"},
                new KWTask() { Message = "Requirement 9"},
                new KWTask() { Message = "Requirement 10"}
            };
        }
    }

}
