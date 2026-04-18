using MercuryTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MercuryTest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<MyOffice_ACPD> MyOffice_ACPD { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MyOffice_ACPD>().HasKey(m => m.ACPD_SID);
        }
    }
}
