using System;
using System.Threading.Tasks;
using Npgsql;

namespace IntegrationTesting.Components.Database
{
    public class PostgresqlConnectionFactory : IAsyncDisposable
    {
        private readonly string _connectionString;
        private NpgsqlConnection _connection;

        public PostgresqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<NpgsqlConnection> Create()
        {
            // not async safe
            if (_connection != null)
                return _connection;

            _connection = new(_connectionString);
            await _connection.OpenAsync();
            return _connection;
        }

        public ValueTask DisposeAsync() => _connection.DisposeAsync();
    }
}