using LicenseService.Models;
using Microsoft.EntityFrameworkCore;

namespace LicenseService.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<License> Licenses { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<License>()
                .HasIndex(x => x.TenantId);
        }
    }
}
