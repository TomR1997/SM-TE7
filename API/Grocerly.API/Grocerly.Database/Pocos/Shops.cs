using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.Database.Pocos
{
    public class Shops
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Zipcde { get; set; }
        public long Longitude { get; set; }
        public long Latitude { get; set; }
        public List<ShopProduct> Products { get; set; }
    }
}
