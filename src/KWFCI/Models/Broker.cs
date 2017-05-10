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
                new KWTask() { Message = "Welcome Email", Type = "Onboarding"},
                new KWTask() { Message = "Winmore", Type = "Onboarding"},
                new KWTask() { Message = "Info Sheet", Type = "Onboarding"},
                new KWTask() { Message = "Account Edge", Type = "Onboarding"},
                new KWTask() { Message = "Google Group", Type = "Onboarding"},
                new KWTask() { Message = "Facebook Group", Type = "Onboarding"},
                new KWTask() { Message = "White Pages MLS", Type = "Onboarding"},
                new KWTask() { Message = "Printer Drivers", Type = "Onboarding"},
                new KWTask() { Message = "Printer Codes", Type = "Onboarding"},
                new KWTask() { Message = "Printer E-Mail", Type = "Onboarding"},
                new KWTask() { Message = "Key Fob", Type = "Onboarding"},
                new KWTask() { Message = "Security Code", Type = "Onboarding"},
                new KWTask() { Message = "Phone Roster", Type = "Onboarding"},
                new KWTask() { Message = "MailBox", Type = "Onboarding"},
                new KWTask() { Message = "RLID Login", Type = "Onboarding"},
                new KWTask() { Message = "Photo", Type = "Onboarding"}
            };
        }
    }

}
