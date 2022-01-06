using Mocking.Units;
using Moq;
using Xunit;
using static Mocking.Units.CreateSomething;

namespace Mocking
{
    public class CreateSomethingTests
    {
        public readonly Mock<IStore> _store = new();

        [Fact]
        public void CreateSomethingResult_NotSuccessful_When()
        {
            CreateSomething createSomething = new(_store.Object);

            var createSomethingResult = createSomething.Create(null);

            Assert.False(createSomethingResult.Success);
            _store.Verify(x => x.Save(It.IsAny<Something>()), Times.Never);
        }

        [Fact]
        public void SaveSomthingToDatabaseWhenValid()
        {
            CreateSomething createSomething = new(_store.Object);
            var something = new Something { Name = "Foo" };
            _store.Setup(x => x.Save(something)).Returns(true);

            var createSomethingResult = createSomething.Create(something);

            Assert.True(createSomethingResult.Success);
            _store.Verify(x => x.Save(something), Times.Once);
        }
    }
}