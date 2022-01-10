using IntegrationTesting.Components.Database;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTesting.Test
{
    [Collection(nameof(AnimalCollection))]
    public class AnimalDatabaseQueryTests
    {
        private readonly AnimalFixture _fixture;
        public AnimalDatabaseQueryTests(AnimalFixture postgresqlFixture)
        {
            _fixture = postgresqlFixture;
        }

        [Fact]
        public async Task AnimalStore_ListsAnimalsFromDatabase()
        {
            var animals = await _fixture._animalStore.GetAnimals();

            Assert.True(animals.Count >= 3);
            Assert.Contains(animals, x => x.Name.Equals("Foo"));
            Assert.Contains(animals, x => x.Name.Equals("Baz"));
            Assert.Contains(animals, x => x.Name.Equals("Tar"));
        }
    }
}
