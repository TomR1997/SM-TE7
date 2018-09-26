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
    public class ShoppingListService : Service
    {
        private GrocerlyContext Orm;
        public ShoppingListService(GrocerlyContext orm)
        {
            this.Orm = orm;
        }

        public HttpResult Get(GetShoppingLists request)
        {
            var shoppingLists = Orm.ShoppingLists.Select(x => FillObject(x)).ToList();
            return new HttpResult(shoppingLists, HttpStatusCode.OK);
        }

        public HttpResult Get(GetShoppingList request)
        {
            var shoppingList = Orm.ShoppingLists.FirstOrDefault(x => x.Id.Equals(request.Id));
            return new HttpResult(FillObject(shoppingList), HttpStatusCode.OK);
        }

        private ShoppingListResponse FillObject(ShoppingLists data)
        {
            return new ShoppingListResponse
            {
                Id = data.Id,
                Name = data.Name,
                Status = data.Status
            };
        }
    }
}
