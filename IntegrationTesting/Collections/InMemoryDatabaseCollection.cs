using IntegrationTesting.Components.Introduction;
using Xunit;

namespace IntegrationTesting.Collections
{
    [CollectionDefinition(nameof(InMemoryDatabaseCollection))]
    public class InMemoryDatabaseCollection : ICollectionFixture<InMemoryDatabaseFixture>
    {
    }
}
