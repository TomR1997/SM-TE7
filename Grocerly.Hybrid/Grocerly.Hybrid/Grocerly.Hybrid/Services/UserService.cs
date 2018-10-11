using Grocerly.Hybrid.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Grocerly.Hybrid.Services
{
    public class UserService
    {
        readonly HttpClient client;
        
        public UserService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri($"{App.BaseApiUrl}/")
            };
        }

        public async Task<User> RegisterAsync(string email, string username, string password, string role)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new User
            {
                Email = email,
                Username = username,
                Password = password,
                Role = role
            }), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"api/users", content);

            var json = await response.Content.ReadAsStringAsync();

            return await Task.Run(() => JsonConvert.DeserializeObject<User>(json));
        }
    }
}
