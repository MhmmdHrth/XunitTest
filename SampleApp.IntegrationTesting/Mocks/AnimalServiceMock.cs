using SampleApp.Models;
using SampleApp.Services;

namespace SampleApp.IntegrationTesting.Mocks
{
    public class AnimalServiceMock : IAnimalService
    {
        public Animal GetAnimal()
        {
            return new()
            {
                Id = 2,
                Name = "Foo2",
                Type = "Bar2",
            };
        }
    }
}
