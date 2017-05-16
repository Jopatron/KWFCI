using System;
using System.ComponentModel.DataAnnotations;

namespace KWFCI.Models
{
    public class Interaction
    {
        public int InteractionID { get; set; }
        public string Notes { get; set; }
        public DateTime? DateCreated { get; set; }

        [StringLength(1000, MinimumLength = 10,ErrorMessage ="Next Step field must be between 10 and 1,000 characters")]
        public string NextStep { get; set; }
        public int? TaskForeignKey { get; set; }
        public string Status { get; set; }
        public KWTask Task { get; set; }
        public string BrokerName { get; set; }


        public Interaction()
        {
            DateCreated = null;
            Status = "Active";
            Notes = "";
            NextStep = "";
        }
    }
}