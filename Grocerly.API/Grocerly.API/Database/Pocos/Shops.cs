using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.Database.Pocos
{
    public class Shops
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        [JsonIgnore]
        public List<ShopProduct> Products { get; set; }
    }
}
