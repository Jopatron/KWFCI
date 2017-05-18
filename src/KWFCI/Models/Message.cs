using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Body { get; set; }
        public StaffProfile From { get; set; }
        public DateTime DateSent { get; set; }
    }
}
