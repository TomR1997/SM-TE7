using Grocerly.API;
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
    public class ShopsControllerTest
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
        public async Task Test_Shops()
        {
            // GET all shops
            var response = await client.GetAsync("/api/shops");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var shops = JsonConvert.DeserializeObject<List<Shops>>(json);

            Assert.That(shops.Count > 0);


            // GET single shop SUCCESS
            response = await client.GetAsync("/api/shops/" + shops[0].Id);
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            var shop = JsonConvert.DeserializeObject<Shops>(json);

            Assert.AreEqual(shop.Id, shops[0].Id);


            // GET single shop BadRequest
            response = await client.GetAsync("/api/shops/" + "no-guid");

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);


            // GET single shop NotFound
            response = await client.GetAsync("/api/shops/" + Guid.NewGuid());

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);


            // PUT shop SUCCESS
            StringContent body = new StringContent(
                JsonConvert.SerializeObject(shop), Encoding.UTF8, "application/json"
                );

            response = await client.PutAsync("/api/shops/" + shop.Id, body);
            response.EnsureSuccessStatusCode();

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);

            // PUT shop BadRequest
            response = await client.PutAsync("/api/shops/" + Guid.NewGuid(), body);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);


            // PUT shop NotFound
            Guid nonExistingId = Guid.NewGuid();

            body = new StringContent(
                JsonConvert.SerializeObject(new Shops
                {
                    Id = nonExistingId,
                    Name = "test1",
                    Latitude = 0.1,
                    Longitude = 0.1,
                    ZipCode = "5500 AB",
                    Products = new List<ShopProduct>()
                }), Encoding.UTF8, "application/json"
                );

            response = await client.PutAsync("/api/shops/" + nonExistingId, body);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);


            // POST shop SUCCESS
            body = new StringContent(
                JsonConvert.SerializeObject(new Shops
                {
                    Name = "test1",
                    Latitude = 0.1,
                    Longitude = 0.1,
                    ZipCode = "5500 AB",
                    Products = new List<ShopProduct>()
                }), Encoding.UTF8, "application/json"
                );

            response = await client.PostAsync("api/shops", body);
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            var newShop = JsonConvert.DeserializeObject<Shops>(json);

            Assert.AreEqual("test1", newShop.Name);


            // POST shop BadRequest 
            body = new StringContent("kaas"
               , Encoding.UTF8, "application/json"
                );

            response = await client.PostAsync("api/shops", body);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);


            // DELETE shop SUCCESS
            response = await client.DeleteAsync("/api/shops/" + shops[0].Id);
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            shop = JsonConvert.DeserializeObject<Shops>(json);

            Assert.AreEqual(shop.Id, shops[0].Id);


            // DELETE single shop BadRequest
            response = await client.DeleteAsync("/api/shops/" + "no-guid");

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);


            // DELETE single shop NotFound
            response = await client.DeleteAsync("/api/shops/" + Guid.NewGuid());

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }
    }
}
