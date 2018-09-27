using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.Database.Pocos
{

    public class ShoppingLists
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public List<ShoppinglistItem> Products { get; set; }
    }
}
