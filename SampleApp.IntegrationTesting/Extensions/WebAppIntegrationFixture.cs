using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SampleApp.IntegrationTesting.Extensions
{
    public class WebAppIntegrationFixture : IAsyncLifetime
    {
        private WebApplicationFactory<Startup> _factory = new WebApplicationFactory<Startup>(); //WebApplicationFactory will mock instance of application

        public HttpClient _client;

        public async Task DisposeAsync()
        {
            _client.Dispose();
            await _factory.DisposeAsync();
        }

        public Task InitializeAsync()
        {
            _factory = _factory.WithWebHostBuilder(x =>
            {
                x.addCustomeService();
            });

            _client = _factory.CreateClient();
            return Task.CompletedTask;
        }
    }
}
