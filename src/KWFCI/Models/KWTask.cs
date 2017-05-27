using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Models
{
    public class KWTask
    {
        public int KWTaskID { get; set; }
        public DateTime? AlertDate { get; set; }
        public int Priority { get; set; } //1-5, 5 = Critical
        [Required(ErrorMessage = "Message is required.")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Message requires between 10 and 1000 characters.")]
        public string Message { get; set; }
        [Required(ErrorMessage = "Date Due is required.")]
        //TODO: compare day to current day or later w/ data annotation
        public DateTime? DateDue { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Type { get; set; } //Task, Onboarding or Alert
        public Interaction Interaction { get; set; }
        public Boolean IsComplete { get; set; }
        public string StaffName { get; set; } //For use in assigning staff to tasks
        public string StaffEmail { get; set; }
        public KWTask()
        {
            DateCreated = DateTime.Now;
            
        }
    }
}
