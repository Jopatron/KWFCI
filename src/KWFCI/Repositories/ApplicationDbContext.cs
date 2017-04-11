using KWFCI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KWFCI.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<StaffUser>
    {
        public DbSet<StaffProfile> StaffProfiles { get; set; }
    }
}
