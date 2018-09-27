using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.Database.Pocos
{
    public class ShopProduct
    {
        public Guid Id_Shop { get; set; }
        public Guid Id_Product { get; set; }

        public Shops Shop { get; set; }
        public Products Product { get; set; }
    }
}
