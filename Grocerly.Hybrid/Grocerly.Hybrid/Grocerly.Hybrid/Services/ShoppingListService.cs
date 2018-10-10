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

        public async Task<IEnumerable<ShoppingListItem>> GetProductsForShoppingList(Guid id)
        {
            var response = await client.GetStringAsync($"api/shoppinglists/" + id + "/products");
            return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<ShoppingListItem>>(response.ToString()));
        }

        public async Task<ShoppingList> CreateShoppingList(string name, Status status)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new ShoppingList
            {
                Name = "New shoppinglist",
                Status = Status.Open
            }), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"api/users", content);

            return await Task.Run(() => JsonConvert.DeserializeObject<ShoppingList>(response.ToString()));
        }

        public async Task<IEnumerable<ShoppingList>> GetShoppingListsForUser(Guid id, Status status)
        {
            var response = await client.GetStringAsync($"api/users/" + id + "/shoppinglists?status=" + status);
            return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<ShoppingList>>(response.ToString()));
        }
    }
}
