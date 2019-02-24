using GlobalMentalityAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMentalityAPI.Data
{
    public class BusinessContext : DbContext
    {
        public BusinessContext(DbContextOptions<BusinessContext> options) : base(options)
        {

        }

        public DbSet<Business> Businesses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<OfficeAdmin> OfficeAdmins { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Business>()
                .HasMany(p => p.Patients)
                .WithOne();

            modelBuilder.Entity<Business>()
                .HasMany(p => p.Doctors)
                .WithOne();

            modelBuilder.Entity<Business>()
                .HasMany(p => p.OfficeAdmins)
                .WithOne();

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
        
}
