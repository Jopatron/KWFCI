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
        public DbSet<Message> Messages { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<KWTask> KWTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<KWTask>()
            .HasOne(i => i.Interaction)
            .WithOne(n => n.Task)
            .HasForeignKey<Interaction>(t => t.TaskForeignKey);
        }
    }
}
