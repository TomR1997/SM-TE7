using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grocerly.API.Database.Pocos
{
    public class ProductsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime CreationDate { get; set; }
        public string Volume { get; set; }
        public int Quantity { get; set; }
    }
}
