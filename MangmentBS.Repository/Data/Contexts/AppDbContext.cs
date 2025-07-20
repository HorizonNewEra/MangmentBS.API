using MangmentBS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Repository.Data.Contexts
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Building> Building { get; set; }
        public DbSet<Flat> Flat { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<FlatContractImages> FlatContractImages { get; set; }
        public DbSet<Installment> Installment { get; set; }
        public DbSet<BuildingContractImages> BuildingContractImages { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
    }
}
