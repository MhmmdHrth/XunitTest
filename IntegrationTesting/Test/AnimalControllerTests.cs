using IntegrationTesting.Components.Introduction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace IntegrationTesting.Test
{
    public class AnimalControllerTests
    {
        private readonly AppDbContext _context;
        public AnimalControllerTests()
        {
            //constructor will execute many time base on number of test!
            DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();
            optionsBuilder.UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name);

            _context = new(optionsBuilder.Options);
            _context.Animals.Add(new Animal { Name = "Foo", Type = "Bar" });
            _context.SaveChanges();
        }

        [Fact]
        public void AnimalController_ListsAnimalsFromDatabase()
        {
            IActionResult result = new AnimalController(_context).List();

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
            IActionResult result = new AnimalController(_context).Get(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var animal = Assert.IsType<Animal>(okResult.Value);
            Assert.NotNull(animal);
            Assert.Equal(1, animal.Id);
            Assert.Equal("Foo", animal.Name);
            Assert.Equal("Bar", animal.Type);
        }
    }
}
