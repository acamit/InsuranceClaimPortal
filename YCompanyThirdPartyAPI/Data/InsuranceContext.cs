using Microsoft.EntityFrameworkCore;
using YCompanyPaymentsAPI.Models;
using YCompanyThirdPartyAPI.Models;

namespace YCompanyPaymentsAPI.Data
{
    
     public class InsuranceContext : DbContext
    {
        public InsuranceContext(DbContextOptions<InsuranceContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Policy>()
                .HasOne(v => v.Vehicle)
                .WithOne(a => a.Policy)
                .HasForeignKey<Vehicle>(a => a.PolicyId);

            modelBuilder.Entity<Policy>()
                .HasMany(v => v.Drivers)
                .WithOne(a => a.Policy)
                .HasForeignKey(a => a.PolicyId);

            modelBuilder.Entity<Policy_Coverage>().HasKey(pc => new { pc.Id});

            modelBuilder.Entity<Policy_Coverage>()
            .HasOne(p => p.Policy)
            .WithMany(a => a.PolicyCoverages)
            .HasForeignKey(a => a.PolicyId)
            .OnDelete(DeleteBehavior.ClientSetNull);            

            modelBuilder.Entity<Policy_Coverage>()
            .HasOne(p => p.Coverage)
            .WithMany(a => a.PolicyCoverages)
            .HasForeignKey(a => a.CoverageId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Vehicle_Driver>().HasKey(vd => new { vd.Id});

            modelBuilder.Entity<Vehicle_Driver>()
            .HasOne(p => p.Vehicle)
            .WithMany(a => a.VehicleDrivers)
            .HasForeignKey(a => a.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Vehicle_Driver>()
            .HasOne(p => p.Driver)
            .WithMany(a => a.VehicleDrivers)
            .HasForeignKey(a => a.DriverId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Vehicle_Coverage>().HasKey(vc => new { vc.Id });

            modelBuilder.Entity<Vehicle_Coverage>()
            .HasOne(p => p.Vehicle)
            .WithMany(a => a.VehicleCoverages)
            .HasForeignKey(a => a.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Vehicle_Coverage>()
            .HasOne(p => p.Coverage)
            .WithMany(a => a.VehicleCoverages)
            .HasForeignKey(a => a.CoverageId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }

}