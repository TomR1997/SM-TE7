using System;
namespace Grocerly.Hybrid.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Zipcode { get; set; }
        public string Address { get; set; }
        public int Housenumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User()
        {
        }
    }

}
