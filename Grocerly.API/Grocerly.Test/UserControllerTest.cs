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
    public class UserControllerTest
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
        public async Task Test_Users()
        {
            // GET all users
            var response = await client.GetAsync("/api/users");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<Users>>(json);

            Assert.That(users.Count > 0);


            // GET single user SUCCESS
            response = await client.GetAsync("/api/users/" + users[0].Id);
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<Users>(json);

            Assert.AreEqual(user.Id, users[0].Id);


            // GET single user BadRequest
            response = await client.GetAsync("/api/users/" + "no-guid");

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);


            // GET single user NotFound
            response = await client.GetAsync("/api/users/" + Guid.NewGuid());

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);


            //// PUT user SUCCESS
            StringContent body = new StringContent(
                JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"
                );

            response = await client.PutAsync("/api/users/" + user.Id, body);
            response.EnsureSuccessStatusCode();

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);

            // PUT user BadRequest
            response = await client.PutAsync("/api/users/" + Guid.NewGuid(), body);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);


            // PUT user NotFound
            Guid nonExistingId = Guid.NewGuid();

            body = new StringContent(
                JsonConvert.SerializeObject(new Users
                {
                    Id = nonExistingId,
                    Name = "test1",
                    Username = "test1",
                    Email = "test1@mail.nl"
                }), Encoding.UTF8, "application/json"
                );

            response = await client.PutAsync("/api/users/" + nonExistingId, body);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);


            // POST user SUCCESS
            body = new StringContent(
                JsonConvert.SerializeObject(new Users
                {
                    Name = "test1",
                    Username = "test1",
                    Email = "test1@mail.nl"
                }), Encoding.UTF8, "application/json"
                );

            response = await client.PostAsync("api/users", body);
            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            var newUser = JsonConvert.DeserializeObject<Users>(json);

            Assert.AreEqual("test1", newUser.Name);


            // POST user BadRequest 
            body = new StringContent("kaas"
               , Encoding.UTF8, "application/json"
                );

            response = await client.PostAsync("api/users", body);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);

            // DELETE single user BadRequest
            response = await client.DeleteAsync("/api/users/" + "no-guid");

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);


            // DELETE single user NotFound
            response = await client.DeleteAsync("/api/users/" + Guid.NewGuid());

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }
    }
}
