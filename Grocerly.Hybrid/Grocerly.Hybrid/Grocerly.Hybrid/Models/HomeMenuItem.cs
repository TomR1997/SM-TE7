using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.Hybrid.Models
{
    public enum MenuItemType
    {
        Browse,
        Shoppinglist
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
