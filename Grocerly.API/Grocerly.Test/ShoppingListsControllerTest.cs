using Grocerly.API;
using Grocerly.API.Database.Pocos;
using Grocerly.Database.Pocos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Grocerly.Test
{
    [TestFixture]
    public class ShoppingListsControllerTest
    {
        private TestServer server;
        private HttpClient client;

        [OneTimeSetUp]
        public void Setup()
        {
            var builder = new WebHostBuilder().UseStartup<Startup>();

            server = new TestServer(builder);
            client = server.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiQWQgbWluaXVzIiwiaWQiOiI5NzMxYTNhOC1iNDVmLTRiMTctODgwMi0yMDE3OTFjOTI5MTQiLCJyb2xlcyI6IkFkbWluIiwibmJmIjoxNTM5ODY2OTU2LCJleHAiOjE1NDA0NzE3NTYsImlhdCI6MTUzOTg2Njk1Nn0.f_8tJGwq6-e84JBp2AJpTcZsQgizbThO1MTiYV1B9nY");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            client.Dispose();
            server.Dispose();
        }

        [Test]
        public async Task Test_ShoppingLists()
        {
            // GET all shoppinglists
            var response = await client.GetAsync("/api/shoppinglists");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var shoppinglists = JsonConvert.DeserializeObject<List<ShoppingLists>>(json);

            Assert.True(shoppinglists.Count > 0);

            // GET single shoppinglist SUCCESS
            response = await client.GetAsync("/api/shoppinglists/" + shoppinglists[0].Id);
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            var shoppinglist = JsonConvert.DeserializeObject<ShoppingLists>(json);

            Assert.AreEqual(shoppinglist.Id, shoppinglists[0].Id);

            // GET single shoppinglist BadRequest
            response = await client.GetAsync("/api/shoppinglists/" + "no-guid");

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);


            // GET single shoppinglist NotFound
            response = await client.GetAsync("/api/shoppinglists/" + Guid.NewGuid());

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);

            // PUT shoppinglist SUCCESS
            StringContent body = new StringContent(
                JsonConvert.SerializeObject(shoppinglist), Encoding.UTF8, "application/json"
                );

            response = await client.PutAsync("/api/shoppinglists/" + shoppinglist.Id, body);
            response.EnsureSuccessStatusCode();

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);

            // PUT shoppinglist BadRequest
            response = await client.PutAsync("/api/shoppinglists/" + Guid.NewGuid(), body);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);

            // PUT shoppinglist NotFound
            Guid nonExistingId = Guid.NewGuid();

            body = new StringContent(
                JsonConvert.SerializeObject(new ShoppingLists
                {
                    Id = nonExistingId,
                    Name = "test1",
                    Products = new List<ShoppinglistItem>(),
                    Status = Status.Open
                }), Encoding.UTF8, "application/json"
                );

            response = await client.PutAsync("/api/shoppinglists/" + nonExistingId, body);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);


            // POST shoppinglist SUCCESS
            response = await client.GetAsync("/api/users");
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<Users>>(json);

            body = new StringContent(
                JsonConvert.SerializeObject(new ShoppingLists
                {
                    Name = "test1",
                    Products = new List<ShoppinglistItem>(),
                    Status = Status.Open
                }), Encoding.UTF8, "application/json"
                );

            response = await client.PostAsync("api/shoppinglists?user_id=" + users[0].Id, body);
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            var newShoppingList = JsonConvert.DeserializeObject<ShoppingLists>(json);

            Assert.AreEqual("test1", newShoppingList.Name);


            // POST shoppinglist BadRequest 
            body = new StringContent("kaas"
               , Encoding.UTF8, "application/json"
                );

            response = await client.PostAsync("api/shoppinglists", body);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);

            // DELETE shoppinglist SUCCESS
            response = await client.DeleteAsync("/api/shoppinglists/" + shoppinglists[0].Id);
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            shoppinglist = JsonConvert.DeserializeObject<ShoppingLists>(json);

            Assert.AreEqual(shoppinglist.Id, shoppinglists[0].Id);


            // DELETE single shoppinglist BadRequest
            response = await client.DeleteAsync("/api/shoppinglists/" + "no-guid");

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);


            // DELETE single shoppinglist NotFound
            response = await client.DeleteAsync("/api/shoppinglists/" + Guid.NewGuid());

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);


        }
    }
}
