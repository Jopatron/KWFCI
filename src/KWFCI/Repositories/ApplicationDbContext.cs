using KWFCI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KWFCI.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<StaffUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<StaffProfile> StaffProfiles { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
    }
}
