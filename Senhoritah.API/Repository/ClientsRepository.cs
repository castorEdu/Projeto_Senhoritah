using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Senhoritah.API.Context;
using Senhoritah.API.Model;

namespace Senhoritah.API.Repository
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly DapperContext _dapperContext;
        public ClientsRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;

        }
        public async Task<IEnumerable<ClientsModel>> FindAll()
        {
            var sql = "SELECT * FROM Clients";
            using (var conn = _dapperContext.CreateConnection())
            {
                var Clients = await conn.QueryAsync<ClientsModel>(sql);
                return Clients.ToList();
            }
        }

        public async Task<ClientsModel> FindClientById(long id)
        {
            var sql = "SELECT * FROM Clients WHERE id = @id";
            using (var conn = _dapperContext.CreateConnection())
            {
                var Clients = await conn.QueryAsync<ClientsModel>(sql, new { id });
                return Clients.FirstOrDefault();
            }
        }
        public async Task<ClientsModel> CreateClient(ClientsModel cli)
        {
            var sqlInsert = "INSERT INTO Clients (Name, Email, PhoneNumber) VALUES (@Name, @Email, @PhoneNumber); SELECT CAST(SCOPE_IDENTITY() as int);";
            using (var conn = _dapperContext.CreateConnection())
            {
                var newClientId = await conn.QuerySingleAsync<int>(sqlInsert, new { cli.Name, cli.Email, cli.PhoneNumber});

                var ClientInserted = "SELECT * FROM Clients WHERE id = @id";
                var client = await conn.QuerySingleAsync<ClientsModel>(ClientInserted, new {id = newClientId });

                return client;
            }
        }
        public async Task<ClientsModel> UpdateClient(ClientsModel cli)
        {
            var sqlUpdate = "UPDATE Clients SET Name = @Name, Email = @Email, PhoneNumber = @PhoneNumber WHERE id = @id";
            using (var conn = _dapperContext.CreateConnection())
            {
                await conn.ExecuteAsync(sqlUpdate, new { cli.Name, cli.Email, cli.PhoneNumber, cli.Id });

                var sqlUpdated = "SELECT * FROM Clients WHERE id = @id";
                var client = await conn.QuerySingleAsync<ClientsModel>(sqlUpdated, new { id = cli.Id });

                return client;
            }
        }

        public async Task<bool> DeleteClient(long id)
        {
            var sql = "DELETE FROM Clients WHERE Id = @Id";

            using (var connection = _dapperContext.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                if (affectedRows > 0) return true;
                return false;
            }
        }

        public async Task<IEnumerable<ClientsModel>> FindByName(string name)
        {
            var sql = "SELECT * FROM Clients WHERE Name LIKE @Name";
            using (var conn = _dapperContext.CreateConnection())
            {
                var Clients = await conn.QueryAsync<ClientsModel>(sql, new { Name = $"%{name}%" });
                return Clients.ToList();
            }
        }
    }
}
