using Grocerly.Database.Pocos;
using Microsoft.EntityFrameworkCore;
using System;

namespace Grocerly.Database
{
    public class GrocerlyContext : DbContext
    {

        public DbSet<Tags> Tags { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ShoppingLists> ShoppingLists { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Shops> Shops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=grocerly.db;");

        }
    }
}
