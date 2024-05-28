using Dapper;
using Microsoft.EntityFrameworkCore;
using Senhoritah.API.Context;
using Senhoritah.API.Model;

namespace Senhoritah.API.Repository
{
    public class BillRepository : IBillRepository
    {
        private readonly DapperContext _dapperContext;
        public BillRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<BillsModel>> FindAll()
        {
            var sql = "SELECT * FROM Bills";
            using (var conn = _dapperContext.CreateConnection())
            {
                var bills = await conn.QueryAsync<BillsModel>(sql);

                return bills.ToList();
            }
        }
        public async Task<BillsModel> Create(BillsModel bill)
        {
            var sql = "INSERT INTO Bills(Name, Price, InsertedAt) VALUES (@Name, @Price, @InsertedAt); SELECT CAST(SCOPE_IDENTITY() as int);";
            using (var conn = _dapperContext.CreateConnection())
            {
                var newIdInserted = await conn.QuerySingleAsync<int>(sql, new { Name = bill.Name, Price = bill.Price, InsertedAt = new DateTime() });

                var sqlSelect = "SELECT * FROM Bills WHERE Id = @Id";
                var newBill = await conn.QuerySingleAsync<BillsModel>(sqlSelect, new { Id = newIdInserted });

                return newBill;
            }
        }
        public async Task<BillsModel> Update(BillsModel bill)
        {
            var sqlUpdate = "UPDATE Bills SET Name = @Name, Price = @Price WHERE Id = @Id";

            using (var conn = _dapperContext.CreateConnection())
            {
                await conn.ExecuteAsync(sqlUpdate, new { Name = bill.Name, Price = bill.Price, Id = bill.Id });

                var sqlSelect = "SELECT * FROM Bills WHERE Id = @Id";
                var bills = await conn.QuerySingleAsync<BillsModel>(sqlSelect, new { Id = bill.Id });

                return bills;
            }
        }
        public async Task<bool> Delete(long id)
        {
            var sql = "DELETE FROM Bills WHERE Id = @Id";

            using (var connection = _dapperContext.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                if (affectedRows > 0) return true;
                return false;
            }
        }

    }
}
