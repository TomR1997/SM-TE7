using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.Database.Pocos
{
    public class ProductTag
    {
        public Guid Id_Product { get; set; }
        public Guid Id_Tag { get; set; }
        public Products Product { get; set; }
        public Tags Tag { get; set; }
    }
}
