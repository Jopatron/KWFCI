using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "You must enter a username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "You must enter a password")]
        public string Password { get; set; }
    }
}
