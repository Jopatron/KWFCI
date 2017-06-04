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
        [Required(ErrorMessage = "Subject is required.")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Body is required.")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Body must have between 10 and 1000 characters.")]
        public string Body { get; set; }
        public string From { get; set; }
        public DateTime DateSent { get; set; }
    }
}
