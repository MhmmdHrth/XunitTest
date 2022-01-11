using IntegrationTesting.Components.Database;
using Xunit;

namespace IntegrationTesting.Collections
{
    [CollectionDefinition(nameof(AnimalCollection))]
    public class AnimalCollection : ICollectionFixture<AnimalFixture>
    {
    }
}
