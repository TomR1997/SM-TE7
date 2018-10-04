using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using Grocerly.Hybrid.Models;
using Newtonsoft.Json;

namespace Grocerly.Hybrid.Services
{
    public class ProductService
    {
        readonly HttpClient client;
        IEnumerable<Product> products;

        public ProductService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri($"{App.BaseApiUrl}/")
            };

            products = new List<Product>();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(
            int numberOfRows, int page, string name
        )
        {

            var values = new NameValueCollection
            {
                {"numberOfRows", numberOfRows.ToString() },
                { "page", page.ToString() },
                { "name", name }
            };
            
            var json = await client.GetStringAsync($"api/item");
            products = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Product>>(json));

            return products;
        }
    }
}
