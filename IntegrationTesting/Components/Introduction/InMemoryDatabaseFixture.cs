using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTesting.Components.Introduction
{
    public class InMemoryDatabaseFixture : IAsyncLifetime
    {
        public AppDbContext _context;

        public async Task DisposeAsync()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.DisposeAsync();
        }

        public async Task InitializeAsync()
        {
            DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();
            optionsBuilder.UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name);
            _context = new AppDbContext(optionsBuilder.Options);

            await _context.Animals.AddAsync(new Animal { Name = "Foo", Type = "Bar" });
            await _context.SaveChangesAsync();
        }
    }
}
