using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KWFCI.Models
{
    public class Person
    {
        [Required(ErrorMessage = "First Name required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email required.")]
        [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
        public string Email { get; set; }
        public bool EmailNotifications { get; set; }
    }
}
