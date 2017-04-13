using System;

namespace KWFCI.Models
{
    public class Alert
    {
        public int AlertID { get; set; }
        public DateTime AlertDate { get; set; }
        public int Priority { get; set; }
        public string Message { get; set; }
    }
}
