using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Models.ViewModels
{
    public class AdminVM
    {
        public List<StaffProfile> Staff { get; set; }
        public StaffProfile NewStaff { get; set; }
        public string Password { get; set; }
    }
}
