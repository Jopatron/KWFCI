using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KWFCI.Models;

namespace KWFCI.Models.ViewModels
{
    public class AlertVM
    {
        public List<Alert> Alerts { get; set; }
        public Alert AddedAlert { get; set; }
    }
}
