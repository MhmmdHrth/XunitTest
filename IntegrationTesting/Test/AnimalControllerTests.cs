using IntegrationTesting.Collections;
using IntegrationTesting.Components.Introduction;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace IntegrationTesting.Test
{
    [Collection(nameof(InMemoryDatabaseCollection))]
    public class AnimalControllerTests
    {
        private readonly InMemoryDatabaseFixture _databaseFixture;
        public AnimalControllerTests(InMemoryDatabaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;
        }

        [Fact]
        public void AnimalController_ListsAnimalsFromDatabase()
        {
            IActionResult result = new AnimalController(_databaseFixture._context).List();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var animals = Assert.IsType<List<Animal>>(okResult.Value);
            var animal = Assert.Single(animals, x => x.Id.Equals(1));
            Assert.NotNull(animal);
            Assert.Equal(1, animal.Id);
            Assert.Equal("Foo", animal.Name);
            Assert.Equal("Bar", animal.Type);
        }

        [Fact]
        public void AnimalController_GetAnimalFromDatabase()
        {
            IActionResult result = new AnimalController(_databaseFixture._context).Get(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var animal = Assert.IsType<Animal>(okResult.Value);
            Assert.NotNull(animal);
            Assert.Equal(1, animal.Id);
            Assert.Equal("Foo", animal.Name);
            Assert.Equal("Bar", animal.Type);
        }
    }
}
