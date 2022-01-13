using SampleApp.IntegrationTesting.Extensions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SampleApp.IntegrationTesting.Tests
{
    [Collection(nameof(WebAppIntegrationCollection))]
    public class FileEndpointTests
    {
        private readonly WebAppIntegrationFixture _fixture;

        public FileEndpointTests(WebAppIntegrationFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task SavesFileToDisk()
        {
            MultipartFormDataContent formData = new();
            formData.Add(new StreamContent(_fixture.TestFile), "file", "base.png"); //second param is the parameter name in controller

            var response = await _fixture._client.PostAsync("/api/files", formData);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var fileResponse = await _fixture._client.GetAsync("/test_images/base.png"); //get image from wwwroot after save
            Assert.Equal(HttpStatusCode.OK, fileResponse.StatusCode);
        }
    }
}
