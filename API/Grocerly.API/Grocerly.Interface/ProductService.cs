using Grocerly.Database;
using Grocerly.ServiceModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;
using Grocerly.Database.Pocos;

namespace Grocerly.Interface
{
    public class ProductService : Service
    {
        private GrocerlyContext Orm;

        public ProductService(GrocerlyContext orm)
        {
            this.Orm = orm;
        }

        public HttpResult Get(GetProducts request)
        {
            var products = Orm.Products; //Orm.Products.Select(x => x)
            return new HttpResult(products, HttpStatusCode.OK);
        }

        public HttpResult GetProduct(GetProduct request)
        {
            var product = Orm.Products.Where(x => x.Id.Equals(request.Id));
            return new HttpResult(product, HttpStatusCode.OK);
        }

        public HttpResult GetProductByName(GetProductByName request)
        {
            var product = Orm.Products.Where(x => x.Name.Equals(request.Name));
            return new HttpResult(product, HttpStatusCode.OK);
        }

        private ProductResponse FillObject(Products data)
        {
            return new ProductResponse
            {
                Id = data.Id,
                Name = data.Name,
                Price = data.Price,
                Quantity = data.Quantity
            };
        }
    }
}
