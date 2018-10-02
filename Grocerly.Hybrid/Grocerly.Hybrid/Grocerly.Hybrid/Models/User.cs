using System;
namespace Grocerly.Hybrid.Models
{
    public class User
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string zipcode { get; set; }
        public string address { get; set; }
        public int houseNumber { get; set; }
        public string username { get; set; }

        public User()
        {
        }
    }

}
