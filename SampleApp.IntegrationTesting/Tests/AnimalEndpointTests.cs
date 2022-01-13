using Microsoft.AspNetCore.Mvc.Testing;
using SampleApp.IntegrationTesting.Extensions;
using SampleApp.Models;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace SampleApp.IntegrationTesting.Tests
{
    [Collection(nameof(WebAppIntegrationCollection))]
    public class AnimalEndpointTests
    {
        private readonly WebAppIntegrationFixture _fixture;
        public AnimalEndpointTests(WebAppIntegrationFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Get_SingleAnimal()
        {
            var response = await _fixture._client.GetAsync("/api/animal");
            var content = await response.Content.ReadAsStringAsync(); //to debug if got error
            var animal = await response.Content.ReadFromJsonAsync<Animal>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(2, animal.Id);
            Assert.NotNull(animal);
        }
    }
}
