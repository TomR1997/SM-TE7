using Grocerly.Hybrid.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Grocerly.Hybrid.Services
{
    public class ShoppingListService
    {
        readonly HttpClient client;
        
        public ShoppingListService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri($"{App.BaseApiUrl}/")
            };
        }

        public async Task<List<Product>> GetProductsForShoppingList(Guid id)
        {
            var response = await client.GetAsync($"api/shoppinglists/" + id + "/products");
            return await Task.Run(() => JsonConvert.DeserializeObject<List<Product>>(response.ToString()));
        }
    }
}
