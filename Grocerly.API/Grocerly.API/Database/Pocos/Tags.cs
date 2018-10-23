using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.Database.Pocos
{
    public class Tags
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<ProductTag> Products { get; set; }
    }
}
