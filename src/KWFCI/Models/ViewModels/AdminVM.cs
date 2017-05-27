using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Models.ViewModels
{
    public class AdminVM
    {
        public List<StaffProfile> Staff { get; set; }
        public StaffProfile NewStaff { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password requires minimum 8 characters, 1 uppercase alphabet, 1 lowercase alphabet, and 1 number")]
        public string Password { get; set; }
        public List<Broker> Brokers { get; set; }
        public Broker NewBroker { get; set; }
        public Interaction NewInteraction { get; set; }
    }
}
