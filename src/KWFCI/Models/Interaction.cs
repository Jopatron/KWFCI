using System;
using System.ComponentModel.DataAnnotations;

namespace KWFCI.Models
{
    public class Interaction
    {
        public int InteractionID { get; set; }
        public string Notes { get; set; }
        public DateTime? DateCreated { get; set; }
        public string NextStep { get; set; }
        public int? TaskForeignKey { get; set; }
        public string Status { get; set; }
        public KWTask Task { get; set; }
        public string BrokerName { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }


        public Interaction()
        {
            DateCreated = null;
            Status = "Active";
            Notes = "";
            NextStep = "";
        }
    }
}