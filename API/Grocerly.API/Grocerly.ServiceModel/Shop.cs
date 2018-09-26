using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grocerly.ServiceModel
{
    [Route("/shops/", "GET")]
    public class GetShops : IReturn<List<ShopResponse>>
    {
    }

    [Route("/shops/{Id}", "GET")]
    public class GetShop : IReturn<ShopResponse>
    {
        public Guid Id { get; set; }
    }
    public class ShopResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public long Longitude { get; set; }
        public long Latitude { get; set; }
    }
}
