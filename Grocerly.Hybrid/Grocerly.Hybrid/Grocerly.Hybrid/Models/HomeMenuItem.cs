using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.Hybrid.Models
{
    public enum MenuItemType
    {
        Browse,
        Shoppinglist,
        Volunteer
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
