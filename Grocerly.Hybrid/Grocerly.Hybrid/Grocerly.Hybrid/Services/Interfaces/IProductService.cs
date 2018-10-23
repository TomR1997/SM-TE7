using Grocerly.Hybrid.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grocerly.Hybrid.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(
            int numberOfRows = 15, int page = 1, string name = ""
        );
    }
}
