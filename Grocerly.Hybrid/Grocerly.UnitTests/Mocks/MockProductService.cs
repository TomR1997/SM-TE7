using Grocerly.Hybrid.Models;
using Grocerly.Hybrid.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocerly.UnitTests
{
    class MockProductService : IProductService
    {
        readonly List<Product> Products = new List<Product>();
        public MockProductService()
        {
            for (int i = 0; i < 20; i++)
            {
                Products.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "AH product " + i,
                    Price = 3.12M,
                    Volume = "500 gr"
                });
            }
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int numberOfRows = 15, int page = 1, string name = "")
        {
            return await Task.FromResult(Products.Where(p => p.Name.Contains(name))
                           .OrderBy(p => p.CreationDate)
                           .Skip(numberOfRows * (page - 1))
                           .Take(numberOfRows));
        }
    }
}
