using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.ServiceModel
{
    [Route("/users/", "GET")]
    public class GetUsers : IReturn<List<UserResponse>>
    {
    }

    [Route("/users/{Id}", "GET")]
    public class GetUser : IReturn<UserResponse>
    {
        public Guid Id { get; set; }
    }

    [Route("/users/{Name}", "GET")]
    public class GetUserByName : IReturn<UserResponse>
    {
        public string Name { get; set; }
    }

    [Route("/users/{Email}", "GET")]
    public class GetUserByEmail : IReturn<UserResponse>
    {
        public string Email { get; set; }
    }
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Zipcode { get; set; }
        public string Address { get; set; }
        public int HouseNumber { get; set; }
    }
}
