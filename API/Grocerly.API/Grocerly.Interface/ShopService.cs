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
    public class ShopService : Service
    {
        private GrocerlyContext Orm;
        public ShopService(GrocerlyContext orm)
        {
            this.Orm = orm;
        }

        public HttpResult Get(GetShops request)
        {
            var shops = Orm.Shops.Select(x => FillObject(x)).ToList();
            return new HttpResult(shops, HttpStatusCode.OK);
        }

        public HttpResult Get(GetShop request)
        {
            var shop = Orm.Shops.FirstOrDefault(x => x.Id.Equals(request.Id));
            return new HttpResult(FillObject(shop), HttpStatusCode.OK);
        }

        private ShopResponse FillObject(Shops data)
        {
            return new ShopResponse
            {
                Id = data.Id,
                Name = data.Name,
                ZipCode = data.ZipCode,
                Latitude = data.Latitude,
                Longitude = data.Longitude
            };
        }
    }
}
