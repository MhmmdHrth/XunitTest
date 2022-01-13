using SampleApp.IntegrationTesting.Extensions;
using Xunit;

namespace SampleApp.IntegrationTesting
{
    [CollectionDefinition(nameof(WebAppIntegrationCollection))]
    public class WebAppIntegrationCollection : ICollectionFixture<WebAppIntegrationFixture>
    {
    }
}
