using Microsoft.EntityFrameworkCore;
using Dapper;
using Senhoritah.API.Context;
using Senhoritah.API.Model;

namespace Senhoritah.API.Repository
{
    public class UnitRepository : IUnitRepository
    {
        private readonly context _context;
        private readonly DapperContext _dapperContext;
        public UnitRepository(context context, DapperContext dapperContext)
        {
            _context = context;
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<UnitsModel>> FindAll()
        {
            var sql = "SELECT * FROM Units";

            using (var conn = _dapperContext.CreateConnection())
            {
                var Units = await conn.QueryAsync<UnitsModel>(sql);
                return Units.ToList();
            }
        }
        public async Task<UnitsModel> FindById(long id)
        {
            var sql = "SELECT * FROM Units WHERE Id = @id";

            using (var conn = _dapperContext.CreateConnection())
            {
                var Units = await conn.QuerySingleAsync<UnitsModel>(sql, new { Id = id });
                return Units;
            }
        }

        public async Task<UnitsModel> FindByName(string Unit)
        {
            var sql = "SELECT * FROM Units WHERE Abbreviation = @Abbreviation";

            using (var conn = _dapperContext.CreateConnection())
            {
                var Units = await conn.QuerySingleAsync<UnitsModel>(sql, new { Abbreviation = Unit});
                return Units;
            }
        }
    }
}
