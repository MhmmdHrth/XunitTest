using Npgsql;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTesting.Components.Database
{
    public class AnimalFixture : IAsyncLifetime
    {
        private const string _connBase = "Server=localhost;Port=5432;User Id=admin;Password=123qwe";
        private const string _db = "test_db";
        private static readonly string _conn = $"{_connBase};Database={_db}";

        private PostgresqlConnectionFactory connectionFactory;
        public IAnimalStore _animalStore { get; private set; }

        public async Task InitializeAsync()
        {
            await DatabaseSetup.CreateDatabase(_connBase, _db);
            connectionFactory = new PostgresqlConnectionFactory(_conn);
            NpgsqlConnection connection = await connectionFactory.Create();
            IDatabase database = new Postgresql(connection);

            _animalStore = new AnimalStore(database);
            await Seed();
        }

        public async Task DisposeAsync()
        {
            await connectionFactory.DisposeAsync();
            await DatabaseSetup.DeleteDatabase(_connBase, _db);
        }

        public async Task Seed()
        {
            await _animalStore.SaveAnimal(new(0, "Foo", "Sheep"));
            await _animalStore.SaveAnimal(new(0, "Baz", "Sheep"));
            await _animalStore.SaveAnimal(new(0, "Tar", "Sheep"));

        }
    }
}
