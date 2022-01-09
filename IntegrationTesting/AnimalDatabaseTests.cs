using IntegrationTesting.Components.Database;
using Npgsql;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTesting
{
    public class AnimalDatabaseTests
    {
        public const string _connBase = "Server=localhost;Port=5432;User Id=admin;Password=123qwe";
        public const string _db = "test_db";
        public static readonly string _conn = $"{_connBase};Database={_db}";

        private readonly PostgresqlConnectionFactory connectionFactory;
        private readonly NpgsqlConnection connection;
        private readonly IDatabase database;

        public AnimalDatabaseTests()
        {
            DatabaseSetup.CreateDatabase(_connBase, _db).GetAwaiter().GetResult();
            connectionFactory = new(_conn);
            connection = connectionFactory.Create().GetAwaiter().GetResult();
            database = new Postgresql(connection);
        }

        [Fact]
        public async Task AnimalStore_SavesAnimalToDatabase()
        {
            IAnimalStore store = new AnimalStore(database);

            await store.SaveAnimal(new(0, "Foo", "Bar"));
            var animals = await store.GetAnimals();

            await connectionFactory.DisposeAsync();
            await DatabaseSetup.DeleteDatabase(_connBase, _db);

            var animal = Assert.Single(animals);
            Assert.NotNull(animal);
            Assert.Equal(1, animal.Id);
            Assert.Equal("Foo", animal.Name);
            Assert.Equal("Bar", animal.Type);
        }
    }
}
