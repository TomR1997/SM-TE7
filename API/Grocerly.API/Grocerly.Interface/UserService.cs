using Grocerly.Database;
using Grocerly.Database.Pocos;
using Grocerly.ServiceModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Grocerly.Interface
{
    public class UserService : Service
    {
        private GrocerlyContext Orm;
        public UserService(GrocerlyContext orm)
        {
            this.Orm = orm;
        }

        public HttpResult Get(GetUsers request)
        {
            var users = Orm.Users.Select(x => FillObject(x)).ToList();
            return new HttpResult(users, HttpStatusCode.OK);
        }

        public HttpResult Get(GetUser request)
        {
            var user = Orm.Users.FirstOrDefault(x => x.Id.Equals(request.Id));
            return new HttpResult(FillObject(user), HttpStatusCode.OK);
        }

        public HttpResult Get(GetUserByName request)
        {
            var user = Orm.Users.FirstOrDefault(x => x.Name.Equals(request.Name));
            return new HttpResult(FillObject(user), HttpStatusCode.OK);
        }

        public HttpResult Get(GetUserByEmail request)
        {
            var user = Orm.Users.FirstOrDefault(x => x.Email.Equals(request.Email));
            return new HttpResult(FillObject(user), HttpStatusCode.OK);
        }

        private UserResponse FillObject(Users data)
        {
            return new UserResponse
            {
                Id = data.Id,
                Name = data.Name,
                Email = data.Email,
                Role = data.Role,
                Zipcode = data.Zipcode,
                Address = data.Address,
                HouseNumber = data.HouseNumber
            };
        }
    }
}
