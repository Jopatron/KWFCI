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
        public string Type { get; set; } //New Broker or In Transition
        public string Status { get; set; } //Active or Inactive

        public Broker()
        {
            Status = "Active";
            if (Type == "New Broker")
                Requirements = CreateRequirementsList();
        }

        public List<KWTask> CreateRequirementsList()
        {
            return new List<KWTask>() {
                new KWTask() { Description = "Requirement 1"},
                new KWTask() { Description = "Requirement 2"},
                new KWTask() { Description = "Requirement 3"},
                new KWTask() { Description = "Requirement 4"},
                new KWTask() { Description = "Requirement 5"},
                new KWTask() { Description = "Requirement 6"},
                new KWTask() { Description = "Requirement 7"},
                new KWTask() { Description = "Requirement 8"},
                new KWTask() { Description = "Requirement 9"},
                new KWTask() { Description = "Requirement 10"}
            };
        }
        //TODO Will probably have to generate the list of requirements in the constructor
    }

}
