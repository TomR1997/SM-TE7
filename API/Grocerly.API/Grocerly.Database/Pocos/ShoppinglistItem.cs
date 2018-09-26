using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.Database.Pocos
{
    public class ShoppinglistItem
    {
        public Guid Id_Shoppinglist { get; set; }
        public Guid Id_Product { get; set; }

        public ShoppingLists List { get; set; }
        public Products Product { get; set; }
    }
}
