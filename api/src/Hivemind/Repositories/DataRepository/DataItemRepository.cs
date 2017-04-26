using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Hivemind.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Hivemind.Repositories.DataRepository
{
    public class DataItemRepository : BaseRepository, IDataItemRepository<DataItem>
    {
        private readonly string _connectionString;

        private IDbConnection Connection => new NpgsqlConnection(this._connectionString);

        public DataItemRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        public async Task Add(DataItem item, CancellationToken token = default(CancellationToken))
        {
            using (var connection = this.Connection)
            {
                connection.Open();
                await connection.ExecuteAsync(this.AsCommand("INSERT INTO Data (CreatedDate,DisplayName) VALUE (@CreatedDate,@DisplayName)", item, token));
            }
        }

        public async Task Remove(int id, CancellationToken token = default(CancellationToken))
        {
            using (var dbConnection = this.Connection)
            {
                dbConnection.Open();
                await dbConnection.ExecuteAsync(this.AsCommand("DELETE FROM Data WHERE Id=@Id", new { Id = id }, token));
            }
        }

        public async Task Update(DataItem item, CancellationToken token = default(CancellationToken))
        {
            using (var dbConnection = this.Connection)
            {
                dbConnection.Open();
                await dbConnection.QueryAsync(this.AsCommand("UPDATE customer SET CreatedDate = @CreatedDate, DisplayName = @DisplayName  WHERE id = @Id", item, token));
            }
        }

        public async Task<DataItem> FindByID(int id, CancellationToken token = default(CancellationToken))
        {
            using (var dbConnection = this.Connection)
            {
                dbConnection.Open();

                return (await dbConnection.QueryAsync<DataItem>(this.AsCommand("SELECT * FROM Data WHERE id = @Id", new { Id = id }, token))).FirstOrDefault();
            }
        }

        public async Task<IEnumerable<DataItem>> FindAll(CancellationToken token = default(CancellationToken))
        {
            using (var dbConnection = this.Connection)
            {
                dbConnection.Open();

                return await dbConnection.QueryAsync<DataItem>(this.AsCommand("SELECT * FROM \"Data\"", token));
            }
        }
    }
}