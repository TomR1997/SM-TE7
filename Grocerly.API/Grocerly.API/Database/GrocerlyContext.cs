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
        public DbSet<ProductTag> ProductTags { get; set; }
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


            //var AHProducts = new List<Products>();

            //for (int i = 0; i < 20; i++)
            //{
            //    AHProducts.Add(new Products
            //    {
            //        Name = "AH product " + i,
            //        Price = 3.12,
            //        Volume = "500 gr"
            //    });
            //}

            //Products.AddRange(AHProducts);

            //for (int i = 0; i < 20; i++)
            //{
            //    AH.Products.Add(
            //        new ShopProduct
            //        {
            //            Shop = AH,
            //            Product = AHProducts[i]
            //        });
            //}

            //for (int i = 0; i < 20; i++)
            //{
            //    Status status = Status.Open;
            //    if (i > 10)
            //    {
            //        status = Status.Closed;
            //        if(i > 15)
            //        {
            //            status = Status.Pending;
            //        }
            //    }

            //    ShoppingLists shoppingList = new ShoppingLists
            //    {
            //        Id = new Guid(),
            //        Name = "Name" + i,
            //        Status = status
            //    };
            //    ShoppingLists.Add(shoppingList);

            //    Users user = new Users
            //    {
            //        Id = new Guid(),
            //        Name = "User " + i,
            //        Email = "Email" + i + "@mail.com",
            //        Role = "User",
            //        Zipcode = "55" + i + "AZ",
            //        Address = "Address" + i,
            //        HouseNumber = i,
            //        Username = "Username" + i,
            //        Password = PasswordHasher.HashPassword("Password" + i),
            //        ShoppingLists = new List<ShoppingLists>()
            //    };

            //    user.ShoppingLists.Add(shoppingList);
            //    Users.Add(user);

            //    ShoppinglistItems.Add(new ShoppinglistItem
            //    {
            //        List = shoppingList,
            //        Product = AHProducts[i],
            //        Quantity = new Random().Next(1, 10)
            //    });
            //}

            var admin = new Users
            {
                Username = "admin",
                Password = PasswordHasher.HashPassword("admin"),
                Role = "Admin",
                Name = "Ad minius"
                
            };

            Users.Add(admin);

            for(int i = 0; i < 5; i++)
            {
                var tag = new Tags
                {
                    Name = "Tag" + i
                };

                Tags.Add(tag);
            }

            var calvePindakaas = new Products
            {
                ImageUrl = "https://i340824core.venus.fhict.nl/media/1/7a4cbe13470b14ea32eaaacc31f5d812.jpg",
                Name = "Calvé Pindakaas",
                Price = 2.55,
                Volume = "350 gram"
            };

            IEnumerable<Tags> tags = new List<Tags>
            {
                new Tags
                {
                    Name = "pindakaas"
                },
                new Tags
                {
                    Name = "met"
                },
                new Tags
                {
                    Name = "pot"
                },
                new Tags
                {
                    Name = "zwarte"
                },
                new Tags
                {
                    Name = "deksel"
                },
                new Tags
                {
                    Name = "calve"
                }
            };
            calvePindakaas.Tags = new List<ProductTag>();
            foreach (Tags t in tags)
            {
                Tags.Add(t);
                calvePindakaas.Tags.Add(
                    new ProductTag
                    {
                        Product = calvePindakaas,
                        Tag = t
                    }
                );
            }
            Products.Add(calvePindakaas);



            var calvePindakaasStukjes = new Products
            {
                ImageUrl = "https://i340824core.venus.fhict.nl/media/1/c145a223740ccb98c540b1ef11ff5e7a.jpg",
                Name = "Calvé Pindakaas Pinda's",
                Price = 2.99,
                Volume = "350 gram"
            };

            tags = new List<Tags>
            {
                new Tags
                {
                    Name = "pindakaas"
                },
                new Tags
                {
                    Name = "pot"
                },
                new Tags
                {
                    Name = "met"
                },
                new Tags
                {
                    Name = "gele"
                },
                new Tags
                {
                    Name = "deksel"
                },
                new Tags
                {
                    Name = "calve"
                }
            };

            calvePindakaasStukjes.Tags = new List<ProductTag>();
            foreach (Tags t in tags)
            {
                Tags.Add(t);
                calvePindakaasStukjes.Tags.Add(
                    new ProductTag
                    {
                        Product = calvePindakaasStukjes,
                        Tag = t
                    }
                );
            }
            Products.Add(calvePindakaasStukjes);

            var ahPindakaas = new Products
            {
                ImageUrl = "https://i340824core.venus.fhict.nl/media/1/d6e2425d58386c13ec5eb4da943714cd.jpg",
                Name = "AH Pindakaas Naturel",
                Price = 1.99,
                Volume = "350 gram"
            };

            tags = new List<Tags>
            {
                new Tags
                {
                    Name = "pindakaas"
                },
                new Tags
                {
                    Name = "met"
                },
                new Tags
                {
                    Name = "pot"
                },
                new Tags
                {
                    Name = "zilvere"
                },
                new Tags
                {
                    Name = "deksel"
                },
                new Tags
                {
                    Name = "albert heijn"
                }
            };

            ahPindakaas.Tags = new List<ProductTag>();
            foreach (Tags t in tags)
            {
                Tags.Add(t);
                ahPindakaas.Tags.Add(
                    new ProductTag
                    {
                        Product = ahPindakaas,
                        Tag = t
                    }
                );
            }
            Products.Add(ahPindakaas);

            var ahStroopwafels = new Products
            {
                ImageUrl = "https://i340824core.venus.fhict.nl/media/1/209713a251040f0904f7457a8afb204f.jpg",
                Name = "AH Stroopwafels",
                Price = 1.59,
                Volume = "12 stuks"
            };

            tags = new List<Tags>
            {
                new Tags
                {
                    Name = "koekjes"
                },
                new Tags
                {
                    Name = "koek"
                },
                new Tags
                {
                    Name = "stroopwafels"
                },
                new Tags
                {
                    Name = "paarse"
                },
                new Tags
                {
                    Name = "zakje"
                },
                new Tags
                {
                    Name = "albert heijn"
                }
            };

            ahStroopwafels.Tags = new List<ProductTag>();
            foreach (Tags t in tags)
            {
                Tags.Add(t);
                ahStroopwafels.Tags.Add(
                    new ProductTag
                    {
                        Product = ahStroopwafels,
                        Tag = t
                    }
                );
            }
            Products.Add(ahStroopwafels);

            var jumboStroopwafels = new Products
            {
                ImageUrl = "https://i340824core.venus.fhict.nl/media/1/a596699b98ef24aef516622d40189b7c.jpg",
                Name = "Jumbo Roomboter Stroopwafels",
                Price = 1.70,
                Volume = "468 gram"
            };

            tags = new List<Tags>
            {
                new Tags
                {
                    Name = "koekjes"
                },
                new Tags
                {
                    Name = "koek"
                },
                new Tags
                {
                    Name = "stroopwafels"
                },
                new Tags
                {
                    Name = "paarse"
                },
                new Tags
                {
                    Name = "zakje"
                },
                new Tags
                {
                    Name = "jumbo"
                }
            };

            jumboStroopwafels.Tags = new List<ProductTag>();
            foreach (Tags t in tags)
            {
                Tags.Add(t);
                jumboStroopwafels.Tags.Add(
                    new ProductTag
                    {
                        Product = jumboStroopwafels,
                        Tag = t
                    }
                );
            }
            Products.Add(jumboStroopwafels);

            var koffieleutjes = new Products
            {
                ImageUrl = "https://i340824core.venus.fhict.nl/media/1/dadb3e2927d7df7096d696d18a79ca76.jpg",
                Name = "Lotus Koffieleutjes",
                Price = 1.39,
                Volume = "235 gram"
            };

            tags = new List<Tags>
            {
                new Tags
                {
                    Name = "koekjes"
                },
                new Tags
                {
                    Name = "koffieleutjes"
                },
                new Tags
                {
                    Name = "koffie"
                },
                new Tags
                {
                    Name = "rode"
                },
                new Tags
                {
                    Name = "speculoos"
                },
                new Tags
                {
                    Name = "lotus"
                }
            };

            koffieleutjes.Tags = new List<ProductTag>();
            foreach (Tags t in tags)
            {
                Tags.Add(t);
                koffieleutjes.Tags.Add(
                    new ProductTag
                    {
                        Product = koffieleutjes,
                        Tag = t
                    }
                );
            }
            Products.Add(koffieleutjes);

            await SaveChangesAsync();
        }
    }
}
