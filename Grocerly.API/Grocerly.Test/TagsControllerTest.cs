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
    public class TagsControllerTest
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
        public async Task Test_Tags()
        {
            // GET all tags
            var response = await client.GetAsync("/api/tags");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var tags = JsonConvert.DeserializeObject<List<Tags>>(json);

            Assert.That(tags.Count > 0);


            // GET single tag SUCCESS
            response = await client.GetAsync("/api/tags/" + tags[0].Id);
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            var tag = JsonConvert.DeserializeObject<Tags>(json);

            Assert.AreEqual(tag.Id, tags[0].Id);


            // GET single tag BadRequest
            response = await client.GetAsync("/api/tags/" + "no-guid");

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);


            // GET single tag NotFound
            response = await client.GetAsync("/api/tags/" + Guid.NewGuid());

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);


            // PUT tag SUCCESS
            StringContent body = new StringContent(
                JsonConvert.SerializeObject(tag), Encoding.UTF8, "application/json"
                );

            response = await client.PutAsync("/api/tags/" + tag.Id, body);
            response.EnsureSuccessStatusCode();

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);

            // PUT tag BadRequest
            response = await client.PutAsync("/api/tags/" + Guid.NewGuid(), body);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);


            // PUT tag NotFound
            Guid nonExistingId = Guid.NewGuid();

            body = new StringContent(
                JsonConvert.SerializeObject(new Tags
                {
                    Id = nonExistingId,
                    Name = "test1",
                    Products = new List<ProductTag>()
                }), Encoding.UTF8, "application/json"
                );

            response = await client.PutAsync("/api/tags/" + nonExistingId, body);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);


            // POST tag SUCCESS
            body = new StringContent(
                JsonConvert.SerializeObject(new Tags
                {
                    Name = "test1",
                    Products = new List<ProductTag>()
                }), Encoding.UTF8, "application/json"
                );

            response = await client.PostAsync("api/tags", body);
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            var newTag = JsonConvert.DeserializeObject<Tags>(json);

            Assert.AreEqual("test1", newTag.Name);


            // POST tag BadRequest 
            body = new StringContent("kaas"
               , Encoding.UTF8, "application/json"
                );

            response = await client.PostAsync("api/tags", body);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);


            // DELETE tag SUCCESS
            response = await client.DeleteAsync("/api/tags/" + tags[0].Id);
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            tag = JsonConvert.DeserializeObject<Tags>(json);

            Assert.AreEqual(tag.Id, tags[0].Id);


            // DELETE single tag BadRequest
            response = await client.DeleteAsync("/api/tags/" + "no-guid");

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);


            // DELETE single tag NotFound
            response = await client.DeleteAsync("/api/tags/" + Guid.NewGuid());

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }
    }
}
