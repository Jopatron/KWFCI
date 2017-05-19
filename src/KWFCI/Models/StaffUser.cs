using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KWFCI.Models
{
    public class StaffUser : IdentityUser
    {
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
