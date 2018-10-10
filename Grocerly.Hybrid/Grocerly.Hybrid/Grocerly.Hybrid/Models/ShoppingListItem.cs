using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.Hybrid.Models
{
    public class ShoppingListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Volume { get; set; }
        public DateTime CreationDate { get; set; }
        public int Quantity { get; set; }
    }
}
