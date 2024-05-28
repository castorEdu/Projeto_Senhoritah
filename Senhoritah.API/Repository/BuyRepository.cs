using Dapper;
using Senhoritah.API.Context;
using Senhoritah.API.Model;

namespace Senhoritah.API.Repository
{
    public class BuyRepository : IBuyRepository
    {
        private DapperContext _dapperContext;
        public BuyRepository(DapperContext dapperContext) 
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<BuyModel>> FindAll()
        {
            var sql = "SELECT * FROM Purchases";
            using (var conn = _dapperContext.CreateConnection()) 
            {
                var buyings = await conn.QueryAsync<BuyModel>(sql);

                return buyings.ToList();
            }
        }
        public async Task<BuyModel> Create(BuyModel buy)
        {
            var sql = "INSERT INTO Purchases(IdProduct, ItemName, IdUnit, Amount, Volume, Weight, Price) VALUES(@IdProduct,@ItemName,@IdUnit,@Amount,@Volume,@Weight,@Price); SELECT CAST(SCOPE_IDENTITY() as int);";
            using (var conn = _dapperContext.CreateConnection())
            {
                var newBuyId = await conn.QuerySingleAsync<int>(sql, new { IdProduct = buy.IdProduct, ItemName = buy.ItemName, IdUnit = buy.IdUnit, Amount = buy.Amount, Volume = buy.Volume, Weight = buy.Weight, Price = buy.Price });

                var sqlSelect = "SELECT * FROM Purchases WHERE Id = @Id";
                var newBuy = await conn.QuerySingleAsync<BuyModel>(sqlSelect, new { Id = buy.Id });
                
                return newBuy;
            }
        }

        public async Task<BuyModel> Update(BuyModel buy)
        {
            var sql = "UPDATE Purchases SET IdProduct = @IdProduct, ItemName = @ItemName, IdUnit = @IdUnit, Amount = @Amount, Volume = @Volume, Weight = @Weight, Price = @Price WHERE Id = @Id";

            using (var conn = _dapperContext.CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { buy.IdProduct, buy.ItemName, buy.IdUnit, buy.Amount, buy.Volume, buy.Weight, buy.Price, buy.Id});

                var sqlSelect = "SELECT * FROM Purchases WHERE Id = @Id";
                var newBuy = await conn.QuerySingleAsync<BuyModel>(sqlSelect, new { Id = buy.Id });

                return newBuy;
            }
        }
    }
}
