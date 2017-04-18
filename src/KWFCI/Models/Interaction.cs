using System;


namespace KWFCI.Models
{
    public class Interaction
    {
        public int InteractionID { get; set; }
        public StaffProfile StaffProfile { get; set; }
        public string Notes { get; set; }
        public DateTime DateCreated { get; set; }
        public string NextStep { get; set; }
        public Broker Broker { get; set; }

        public Interaction()
        {
            StaffProfile = Helper.StaffProfileLoggedIn;
            DateCreated = DateTime.Now;
        }
    }
}