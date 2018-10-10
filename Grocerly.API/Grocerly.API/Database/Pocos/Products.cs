using Newtonsoft.Json;
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
        public string Volume { get; set; }
        public DateTime CreationDate { get; set; }

        [JsonIgnore]
        public List<ProductTag> Tags { get; set; }
        [JsonIgnore]
        public List<ShoppinglistItem> Lists { get; set; }
        [JsonIgnore]
        public List<ShopProduct> Shops { get; set; }
    }
}
