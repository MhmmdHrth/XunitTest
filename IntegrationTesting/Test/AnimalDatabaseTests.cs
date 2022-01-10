using IntegrationTesting.Components;
using System;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTesting.Test
{
    [Collection(nameof(AnimalCollection))]
    public class AnimalDatabaseTests //: IClassFixture<AnimalFixture>
    {
        private readonly AnimalFixture _fixture;
        public AnimalDatabaseTests(AnimalFixture postgresqlFixture)
        {
            _fixture = postgresqlFixture;
        }

        [Fact]
        public async Task AnimalStore_SavesAnimalToDatabase()
        {
            var name = Guid.NewGuid().ToString();
            await _fixture._animalStore.SaveAnimal(new(0, name, "Bar"));

            var animals = await _fixture._animalStore.GetAnimals();

            Assert.Single(animals, x => x.Name.Equals(name));
        }

        [Fact]
        public async Task AnimalStore_GetsSavedAnimalByIdFromDatabase()
        {
            var animal = await _fixture._animalStore.GetAnimal(1);

            Assert.NotNull(animal);
            Assert.Equal(1, animal.Id);
            Assert.Equal("Foo", animal.Name);
            Assert.Equal("Sheep", animal.Type);
        }
    }
}
