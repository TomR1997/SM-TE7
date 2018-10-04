using Grocerly.API.Database.Pocos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.Database.Pocos
{

    public class ShoppingLists
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }

        [JsonIgnore]
        public List<ShoppinglistItem> Products { get; set; }
    }
}
