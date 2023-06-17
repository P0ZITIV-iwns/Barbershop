using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Barbershop
{
    public class DbAppContext : DbContext
    {
        public DbSet<Service> Service { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Entry> Entry { get; set; }
        public DbSet<Finance> Finance { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Username=postgres;Password=root;Database=BarbershopDatabase");
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>().HasOne(p => p.ClientEntity).WithMany(p => p.EntryEntities);
            modelBuilder.Entity<Entry>().HasOne(p => p.EmployeeEntity).WithMany(p => p.EntryEntities);
            modelBuilder.Entity<Entry>().HasOne(p => p.ServiceEntity).WithMany(p => p.EntryEntities);
            modelBuilder.Entity<Finance>().HasOne(p => p.EntryEntity).WithOne(p => p.FinanceEntities);
            //modelBuilder.Entity<Finance>().HasOne(p => p.ProductEntity).WithMany(p => p.FinanceEntities);
        }
    }
}
