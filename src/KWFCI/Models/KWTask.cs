using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Models
{
    public class KWTask
    {
        public int KWTaskID { get; set; }
        public string Description { get; set; }
        public Alert Alert { get; set; }
        public StaffProfile StaffProfile { get; set; }
    }
}
