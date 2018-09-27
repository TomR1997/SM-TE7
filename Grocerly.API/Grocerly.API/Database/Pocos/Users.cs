using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.Database.Pocos
{
    public class Users
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Zipcode { get; set; }
        public string Address { get; set; }
        public int HouseNumber { get; set; }

        public List<ShoppingLists> ShoppingLists { get; set; }
    }
}
