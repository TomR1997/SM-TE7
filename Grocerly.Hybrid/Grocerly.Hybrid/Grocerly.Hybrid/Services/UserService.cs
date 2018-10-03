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

        public async Task<User> RegisterAsync(string email, string username, string password)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                Email = email,
                Username = username,
                Password = password
            }), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://i315103core.venus.fhict.nl/api/users", content).ConfigureAwait(false);

            return await Task.Run(() => JsonConvert.DeserializeObject<User>(response.Content.ToString()));
        }
    }
}
