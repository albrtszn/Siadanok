using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Entity;
using Siadanok.Models;

namespace DataBase
{
    public class EFDBContext : DbContext
    {
        public DbSet<User> userRepo { get; set; }
        public DbSet<Manager> managerRepo { get; set; }
        public DbSet<Admin> adminRepo { get; set; }
        public DbSet<Item> itemRepo { get; set; }
        public DbSet<CartItem> cartItemsRepo { get; set; }
        public DbSet<DeliveryOrder> deliveryOrderRepo { get; set; }
        public DbSet<ReserveOrder> reserveOrderRepo { get; set; }
        public DbSet<Role> roleRepo { get; set; }
        public DbSet<UserRole> userRoleRepo { get; set; }
        //public DbSet<Comment> commentRepo { get; set; }

        public EFDBContext(DbContextOptions<EFDBContext> options) : base(options) { }

    }

    public class EFDBContextFactory : IDesignTimeDbContextFactory<EFDBContext>
    {
        public EFDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EFDBContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SiadanokDb;Trusted_Connection=True;MultipleActiveResultSets=true", x => x.MigrationsAssembly("DataBase"));
            return new EFDBContext(optionsBuilder.Options);
        }
    }
}