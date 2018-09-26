using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.ServiceModel
{
    [Route("/products/", "GET")]
    public class GetProducts : IReturn<List<ProductResponse>>
    {
    }

    [Route("/products/{Id}", "GET")]
    public class GetProduct : IReturn<ProductResponse>
    {
        public Guid Id { get; set; }
    }

    [Route("/products/{Name}", "GET")]
    public class GetProductByName : IReturn<ProductResponse>
    {
        public string Name { get; set; }
    }

    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
