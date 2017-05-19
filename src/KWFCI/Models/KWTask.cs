using System;
using System.ComponentModel.DataAnnotations;


namespace KWFCI.Models
{
    public class KWTask
    {
        public int KWTaskID { get; set; }
        public DateTime? AlertDate { get; set; }
        public int Priority { get; set; } //1-5, 5 = Critical
        public string Message { get; set; }
        public DateTime? DateDue { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Type { get; set; } //Task, Onboarding or Alert
        public Interaction Interaction { get; set; }
        public Boolean IsComplete { get; set; }
        public string StaffName { get; set; } //For use in assigning staff to tasks
        public string StaffEmail { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public KWTask()
        {
            DateCreated = DateTime.Now;
            
        }
    }
}
