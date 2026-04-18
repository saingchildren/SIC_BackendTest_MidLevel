using MercuryTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MercuryTest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<MyOffice_ACPD> MyOffice_ACPD { get; set; }
        public DbSet<MyOffice_ExcuteionLog> MyOffice_ExcuteionLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MyOffice_ACPD>().HasKey(m => m.ACPD_SID);
            modelBuilder.Entity<MyOffice_ExcuteionLog>().HasKey(m => m.DeLog_AutoID);
        }
    }
}
