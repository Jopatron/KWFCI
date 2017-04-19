using System;


namespace KWFCI.Models
{
    public class Interaction
    {
        public int InteractionID { get; set; }
        public string Notes { get; set; }
        public DateTime DateCreated { get; set; }
        public string NextStep { get; set; }
        public string Status { get; set; }

        public Interaction()
        {
            DateCreated = DateTime.Now;
            Status = "Active";
        }
    }
}