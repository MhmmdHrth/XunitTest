using Mocking.Units;
using Moq;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Mocking
{
    public class DontTestMicrosoftApiTests
    {
        public readonly Mock<IFiles> _fileMock = new();

        [Fact]
        public async Task WritesToFileStream()
        {
            var service = new DontTestMicrosoftApi(_fileMock.Object);
            var memoryStream = new MemoryStream();
            _fileMock.Setup(x => x.OpenWriteStreamTo("path")).Returns(memoryStream);

            await service.SaveFile("path", new MemoryStream(new byte[] { 2, 3, 4, 5 }));

            Assert.Equal(4, memoryStream.Length);
        }
    }
}
