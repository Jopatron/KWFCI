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
        public int Priority { get; set; }
        public string Message { get; set; }
    }
}
