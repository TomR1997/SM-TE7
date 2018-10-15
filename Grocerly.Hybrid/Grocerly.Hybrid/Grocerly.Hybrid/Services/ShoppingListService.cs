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

        public async Task<IEnumerable<ShoppingList>> GetShoppingListsWithStatus(Status status)
        {
            var json = await client.GetStringAsync($"api/shoppinglists/?status=" + status);

            return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<ShoppingList>>(json));
        }

        public async Task<IEnumerable<ShoppingListItem>> GetProductsForShoppingList(Guid id)
        {
            var response = await client.GetStringAsync($"api/shoppinglists/" + id + "/products");


            return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<ShoppingListItem>>(response));
        }

        public async Task<ShoppingList> CreateShoppingList()
        {
            var content = new StringContent(JsonConvert.SerializeObject(new ShoppingList
            {
                Name = "New shoppinglist",
                Status = Status.Open
            }), Encoding.UTF8, "application/json");

            var url = string.Format("api/shoppinglists?user_id={0}", App.user.Id);

            var response = await client.PostAsync(url, content);

            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new ArgumentException();

            return await Task.Run(() => JsonConvert.DeserializeObject<ShoppingList>(json));
        }

        public async Task<Product> AddProductToList(Guid product_id, Guid shoppingList_id)
        {
            var response = await client.PostAsync($"api/shoppinglists/" + shoppingList_id + "/products/" + product_id, null);
            string json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new ArgumentException();

            return await Task.Run(() => JsonConvert.DeserializeObject<Product>(json));
        }

        public async Task<IEnumerable<ShoppingList>> GetShoppingListsForUser(Guid id, Status status)
        {
            var response = await client.GetStringAsync($"api/users/" + id + "/shoppinglists?status=" + status);
            return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<ShoppingList>>(response));
        }

        public async Task<bool> UpdateShoppingList(ShoppingList shoppingList)
        {
            var content = new StringContent(JsonConvert.SerializeObject(shoppingList), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"api/shoppinglists/" + shoppingList.Id, content);

            var json = response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }

        public async Task<ShoppingList> GetShoppingList(Guid id)
        {
            var response = await client.GetStringAsync($"api/shoppinglists/" + id);
            return await Task.Run(() => JsonConvert.DeserializeObject<ShoppingList>(response));
        }
    }
}
