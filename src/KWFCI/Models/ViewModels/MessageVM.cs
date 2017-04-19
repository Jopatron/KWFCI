using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KWFCI.Models;

namespace KWFCI.Models.ViewModels
{
    public class MessageVM
    {
        public List<Message> Messages { get; set; }
        public Message NewMessage { get; set; }
    }
}
