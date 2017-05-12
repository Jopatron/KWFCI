using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Models.ViewModels
{
    public class HomeVM
    {
        public List<KWTask> GlobalTasks { get; set; }
        public List<KWTask> PersonalTasks { get; set; }
        public List<Interaction> PersonalInteractions { get; set; }
        public Broker NewBroker { get; set; }
        public KWTask NewTask { get; set; }

    }
}
