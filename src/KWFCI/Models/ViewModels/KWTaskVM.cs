using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Models.ViewModels
{
    public class KWTaskVM
    {
        public List<KWTask> KWTasks { get; set; }
        public KWTask NewKWTask { get; set; }
        public List<StaffProfile> StaffList { get; set; }
    }
}
