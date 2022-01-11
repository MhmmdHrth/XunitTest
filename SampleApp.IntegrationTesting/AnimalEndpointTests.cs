using Microsoft.AspNetCore.Mvc.Testing;
using SampleApp.Models;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace SampleApp.IntegrationTesting
{
    public class AnimalEndpointTests : IClassFixture<WebApplicationFactory<Startup>> //WebApplicationFactory will mock instance of application
    {
        private readonly WebApplicationFactory<Startup> _applicationFactory;
        public AnimalEndpointTests(WebApplicationFactory<Startup> applicationFactory)
        {
            _applicationFactory = applicationFactory.WithWebHostBuilder(x =>
            {
                x.addCustomeService();
            });
        }

        [Fact]
        public async Task Get_SingleAnimal()
        {
            var client = _applicationFactory.CreateClient();

            var response = await client.GetAsync("/api/animal");
            var content = await response.Content.ReadAsStringAsync(); //to debug if got error
            var animal = await response.Content.ReadFromJsonAsync<Animal>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(2, animal.Id);
            Assert.NotNull(animal);
        }
    }
}
