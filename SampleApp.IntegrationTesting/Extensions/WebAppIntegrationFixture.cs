using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SampleApp.IntegrationTesting.Extensions
{
    public class WebAppIntegrationFixture : IAsyncLifetime
    {
        private WebApplicationFactory<Startup> _factory = new WebApplicationFactory<Startup>(); //WebApplicationFactory will mock instance of application

        public HttpClient _client;

        public Stream TestFile { get; private set; }

        private string _clenupPath { get; set; }

        public async Task DisposeAsync()
        {
            _client.Dispose();
            await _factory.DisposeAsync();

            var directoryInfo = new DirectoryInfo(_clenupPath);
            foreach(var file in directoryInfo.GetFiles())
            {
                file.Delete();
            }

            Directory.Delete(_clenupPath);
        }

        public async Task InitializeAsync()
        {
            _factory = _factory.WithWebHostBuilder(x =>
            {
                (IWebHostBuilder builder, string cleanupPath) = x.addCustomeService();
                _clenupPath = cleanupPath;
            });

            _client = _factory.CreateClient();

            TestFile = await GetTestImage();
        }

        public async Task<Stream> GetTestImage()
        {
            MemoryStream st = new();
            var fileStream = File.OpenRead("base.png");
            await fileStream.CopyToAsync(st);

            return st;
        }
    }
}
