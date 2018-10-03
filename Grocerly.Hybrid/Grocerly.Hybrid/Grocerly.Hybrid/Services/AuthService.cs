using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Grocerly.Hybrid.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Grocerly.Hybrid.Services
{
    public class AuthService
    {
        readonly HttpClient client;

        public AuthService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri($"{App.BaseApiUrl}/")
            };
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var parameters = String.Format("?username={0}&password={1}", username, password);

            client.DefaultRequestHeaders.Add("Content", "application/json");

            var response = await client.PostAsync($"api/auth" + parameters, null);

            if (response.Headers.TryGetValues("Authorization", out IEnumerable<string> values))
            {
                Application.Current.Properties["jwt"] = values.First();
                await Application.Current.SavePropertiesAsync();
            }

            if (!response.IsSuccessStatusCode)
                throw new UnauthorizedAccessException();

            var json = await response.Content.ReadAsStringAsync();

            return await Task.Run(() => JsonConvert.DeserializeObject<User>(json));
        }
    }
}
