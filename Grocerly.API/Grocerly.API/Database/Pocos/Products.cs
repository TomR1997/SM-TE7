using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.Database.Pocos
{
    public class Products
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public List<ProductTag> Tags { get; set; }
        public List<ShoppinglistItem> Lists { get; set; }
        public List<ShopProduct> Shops { get; set; }
    }
}
