using System;

namespace KWFCI.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public StaffProfile From { get; set; }
        public DateTime DateSent { get; set; }

    }
}
