using System;


namespace KWFCI.Models
{
    public class Interaction
    {
        public int InteractionID { get; set; }
        public StaffProfile StaffMember { get; set; }
        public string Notes { get; set; }
        public DateTime DateCreated { get; set; }
        public string NextStep { get; set; }

        public Interaction()
        {
            StaffMember = Helper.StaffProfileLoggedIn;
            DateCreated = DateTime.Now;
        }
    }
}