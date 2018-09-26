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
            var products = Orm.Products.ToList();
            return new HttpResult(products, HttpStatusCode.OK);

            //var products = (from s in Orm.Products
            //                select FillObject(s)).ToList();
        }

        public HttpResult Get(GetProduct request)
        {
            var product = Orm.Products.FirstOrDefault(x => x.Id.Equals(request.Id));
            return new HttpResult(FillObject(product), HttpStatusCode.OK);

            /*var product = (from s in Orm.Products
                           where s.Id.Equals(request.Id)
                           select FillObject(s));
            return new HttpResult(product, HttpStatusCode.OK);*/
        }

        public HttpResult Get(GetProductByName request)
        {
            var product = Orm.Products.FirstOrDefault(x => x.Name.Equals(request.Name));
            return new HttpResult(FillObject(product), HttpStatusCode.OK);

            /*var product = (from s in Orm.Products
                           where s.Name.Equals(request.Name)
                           select FillObject(s));
            return new HttpResult(product, HttpStatusCode.OK);*/
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
