using Grocerly.API.Database.Pocos;
using Grocerly.API.Utils;
using Grocerly.Database.Pocos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Grocerly.Database
{
    public class GrocerlyContext : DbContext
    {

        public DbSet<Tags> Tags { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ShoppingLists> ShoppingLists { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Shops> Shops { get; set; }

        public DbSet<ShopProduct> ShopProducts { get; set; }
        public DbSet<ShoppinglistItem> ShoppinglistItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=grocerly.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasAlternateKey(u => u.Username)
                .HasName("Unique_Username");

            modelBuilder.Entity<ProductTag>()
            .HasKey(pt => new { pt.Id_Product, pt.Id_Tag });

            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.Tags)
                .HasForeignKey(pt => pt.Id_Product);

            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.Products)
                .HasForeignKey(pt => pt.Id_Tag);

            modelBuilder.Entity<ShoppinglistItem>()
           .HasKey(sl => new { sl.Id_Product, sl.Id_Shoppinglist });

            modelBuilder.Entity<ShoppinglistItem>()
                .HasOne(sl => sl.Product)
                .WithMany(p => p.Lists)
                .HasForeignKey(sl => sl.Id_Product);

            modelBuilder.Entity<ShoppinglistItem>()
                .HasOne(sl => sl.List)
                .WithMany(i => i.Products)
                .HasForeignKey(sl => sl.Id_Shoppinglist);

            modelBuilder.Entity<ShopProduct>()
           .HasKey(sp => new { sp.Id_Product, sp.Id_Shop });

            modelBuilder.Entity<ShopProduct>()
                .HasOne(sp => sp.Product)
                .WithMany(p => p.Shops)
                .HasForeignKey(sl => sl.Id_Product);

            modelBuilder.Entity<ShopProduct>()
                .HasOne(sp => sp.Shop)
                .WithMany(p => p.Products)
                .HasForeignKey(sp => sp.Id_Shop);

            modelBuilder.Entity<Users>()
                .HasMany(u => u.ShoppingLists)
                .WithOne(sl => sl.User);

            modelBuilder.Entity<Products>()
                        .Property(p => p.CreationDate)
                        .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S','now')");
        }

        public async void CreateDummyData()
        {
            var AH = new Shops
            {
                Name = "Albert Heijn Strijpsestraat 137a",
                ZipCode = "5616GM",
                Latitude = 51.4407741,
                Longitude = 5.4531073,
                Products = new List<ShopProduct>()
            };

            var Lidl = new Shops
            {
                Name = "Lidl Woenselse Markt 38",
                ZipCode = " 5612CS",
                Latitude = 51.4507828,
                Longitude = 5.4718054,
                Products = new List<ShopProduct>()
            };

            Shops.Add(AH);
            Shops.Add(Lidl);


            var AHProducts = new List<Products>();

            for (int i = 0; i < 20; i++)
            {
                AHProducts.Add(new Products
                {
                    Name = "AH product " + i,
                    Price = 3.12,
                    Volume = "500 gr"
                });
            }

            Products.AddRange(AHProducts);

            for (int i = 0; i < 20; i++)
            {
                AH.Products.Add(
                    new ShopProduct
                    {
                        Shop = AH,
                        Product = AHProducts[i]
                    });
            }

            for (int i = 0; i < 20; i++)
            {
                ShoppingLists shoppingList = new ShoppingLists
                {
                    Id = new Guid(),
                    Name = "Name" + i,
                    Status = (Status)Enum.GetValues(typeof(Status)).GetValue(new Random().Next(Enum.GetValues(typeof(Status)).Length))

                };
                ShoppingLists.Add(shoppingList);

                Users user = new Users
                {
                    Id = new Guid(),
                    Name = "User " + i,
                    Email = "Email" + i + "@mail.com",
                    Role = "User",
                    Zipcode = "55" + i + "AZ",
                    Address = "Address" + i,
                    HouseNumber = i,
                    Username = "Username" + i,
                    Password = PasswordHasher.HashPassword("Password" + i),
                    ShoppingLists = new List<ShoppingLists>()
                };

                user.ShoppingLists.Add(shoppingList);
                Users.Add(user);

                ShoppinglistItems.Add(new ShoppinglistItem
                {
                    List = shoppingList,
                    Product = AHProducts[i],
                    Quantity = new Random().Next(1, 10)
                });
            }

            await SaveChangesAsync();
        }
    }
}
