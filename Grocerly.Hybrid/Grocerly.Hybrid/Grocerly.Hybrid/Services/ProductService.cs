using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Grocerly.Hybrid.Models;
using Grocerly.Hybrid.Services.Interfaces;
using Newtonsoft.Json;

namespace Grocerly.Hybrid.Services
{
    public class ProductService : IProductService
    {
        readonly HttpClient client;
        IEnumerable<Product> products;

        public ProductService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri($"{App.BaseApiUrl}/")
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.jwt);

            products = new List<Product>();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(
            int numberOfRows = 15, int page = 1, string name = ""
        )
        {
            var builder = new UriBuilder(client.BaseAddress + "api/products");
            var query = HttpUtility.ParseQueryString(builder.Query);

            query["numberOfRows"] = numberOfRows.ToString();
            query["page"] = page.ToString();
            if (!string.IsNullOrEmpty(name))
                query["name"] = name;

            builder.Query = query.ToString();

            var json = await client.GetStringAsync(builder.ToString());
            products = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Product>>(json));

            return products;
        }
    }
}
