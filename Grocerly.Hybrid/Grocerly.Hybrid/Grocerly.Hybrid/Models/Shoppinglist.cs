using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.Hybrid.Models
{
    public class ShoppingList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
    }
}
