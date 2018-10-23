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
    public class ProductsControllerTest
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
        public async Task Test_Products()
        {
            // GET all products
            var response = await client.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Products>>(json);

            Assert.That(products.Count == 15);


            // GET single product SUCCESS
            response = await client.GetAsync("/api/products/" + products[0].Id);
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Products>(json);

            Assert.AreEqual(product.Id, products[0].Id);


            // GET single product BadRequest
            response = await client.GetAsync("/api/products/" + "no-guid");

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);


            // GET single product NotFound
            response = await client.GetAsync("/api/products/" + Guid.NewGuid());

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);


            // PUT product SUCCESS
            StringContent body = new StringContent(
                JsonConvert.SerializeObject(product),Encoding.UTF8, "application/json"
                );

            response = await client.PutAsync("/api/products/" + product.Id, body);
            response.EnsureSuccessStatusCode();

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);

            // PUT product BadRequest
            response = await client.PutAsync("/api/products/" + Guid.NewGuid(), body);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);


            // PUT product NotFound
            Guid nonExistingId = Guid.NewGuid();

            body = new StringContent(
                JsonConvert.SerializeObject(new Products
                {
                    Id = nonExistingId,
                    Name = "test1",
                    Price = 3.12,
                    Volume = "3 stuks"
                }), Encoding.UTF8, "application/json"
                );

            response = await client.PutAsync("/api/products/" + nonExistingId, body);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);


            // POST product SUCCESS
            body = new StringContent(
                JsonConvert.SerializeObject(new Products {
                    Name = "test1",
                    Price = 3.12,
                    Volume = "3 stuks"
                }), Encoding.UTF8, "application/json"
                );

            response = await client.PostAsync("api/products", body);
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            var newProduct = JsonConvert.DeserializeObject<Products>(json);

            Assert.AreEqual("test1", newProduct.Name);


            // POST product BadRequest 
            body = new StringContent("kaas"
               , Encoding.UTF8, "application/json"
                );

            response = await client.PostAsync("api/products", body);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);


            // DELETE product SUCCESS
            response = await client.DeleteAsync("/api/products/" + products[0].Id);
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            product = JsonConvert.DeserializeObject<Products>(json);

            Assert.AreEqual(product.Id, products[0].Id);


            // DELETE single product BadRequest
            response = await client.DeleteAsync("/api/products/" + "no-guid");

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);


            // DELETE single product NotFound
            response = await client.DeleteAsync("/api/products/" + Guid.NewGuid());

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }
    }
}
