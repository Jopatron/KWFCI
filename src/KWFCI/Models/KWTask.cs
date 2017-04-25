using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Models
{
    public class KWTask
    {
        public int KWTaskID { get; set; }
        public DateTime AlertDate { get; set; }
        public int Priority { get; set; } //1-6, 6 = Critical
        public string Message { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime DateCreated { get; set; }
        public string Type { get; set; } //Task or Alert

        public KWTask()
        {
            DateCreated = DateTime.Now;
            Type = "Task";
        }
    }
}
