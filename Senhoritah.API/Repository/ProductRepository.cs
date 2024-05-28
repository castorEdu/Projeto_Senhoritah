using Dapper;
using Microsoft.EntityFrameworkCore;
using Senhoritah.API.Context;
using Senhoritah.API.Model;

namespace Senhoritah.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContext _dapperContext;
        public ProductRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<ProductModel>> FindAll()
        {
            var sql = "SELECT * FROM Products";
            using (var conn = _dapperContext.CreateConnection())
            {
                var products = await conn.QueryAsync<ProductModel>(sql);
                foreach(var un in products)
                {
                    var QueryUn = "SELECT * FROM Units WHERE id=@Id";
                    var unit = await conn.QuerySingleAsync<UnitsModel>(QueryUn, new { Id = un.IdUnit });
                    un.Unit = unit;
                }
                return products.ToList();
            }
        }
        public async Task<ProductModel> FindProductById(long id)
        {
            var sql = "SELECT * FROM Products Where id = @Id";
            using (var conn = _dapperContext.CreateConnection())
            {
                var products = await conn.QuerySingleAsync<ProductModel>(sql,new { Id = id });
                return products;
            }
        }
        public async Task<ProductModel> CreateProduct(ProductModel prod)
        {
            var sqlInsert = "INSERT INTO Products (Name, ProductCategory, Brand, IdUnit) VALUES (@Name, @ProductCategory, @Brand, @IdUnit); SELECT CAST(SCOPE_IDENTITY() as int);";

            using (var conn = _dapperContext.CreateConnection())
            {
                var newProductId = await conn.QuerySingleAsync<int>(sqlInsert, new { prod.Name, prod.ProductCategory, prod.Brand, prod.IdUnit });

                var sqlSelect = "SELECT * FROM Products WHERE Id = @Id";
                var product = await conn.QuerySingleAsync<ProductModel>(sqlSelect, new { Id = newProductId });

                return product;
            }
        }
        public async Task<ProductModel> UpdateProduct(ProductModel prod)
        {
            var sqlUpdate = "UPDATE Products SET Name = @Name, ProductCategory = @ProductCategory, Brand = @Brand, IdUnit = @IdUnit WHERE Id = @Id";

            using (var conn = _dapperContext.CreateConnection())
            {
                await conn.ExecuteAsync(sqlUpdate, new { prod.Name, prod.ProductCategory, prod.Brand, prod.IdUnit, prod.Id });

                var sqlSelect = "SELECT * FROM Products WHERE Id = @Id";
                var product = await conn.QuerySingleAsync<ProductModel>(sqlSelect, new { Id = prod.Id });

                return product;
            }
        }
        public async Task<bool> DeleteProduct(long id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id";

            using (var connection = _dapperContext.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                if(affectedRows > 0) return true;
                return false;
            }
        }

        public async Task<IEnumerable<ProductModel>> FindByName(string name)
        {
            var sql = "SELECT * FROM Products WHERE Name LIKE @Name";

            using (var conn = _dapperContext.CreateConnection())
            {
                var products = await conn.QueryAsync<ProductModel>(sql, new {Name = $"%{ name }%" });
                foreach(var p in products)
                {
                    var QueryUn = "SELECT * FROM Units WHERE id=@Id";
                    var unit = await conn.QuerySingleAsync<UnitsModel>(QueryUn, new { Id = p.IdUnit });
                    p.Unit = unit;
                }
                return products.ToList();
            }
        }

        public async Task<IEnumerable<ProductsRecipeModel>> FindProductInRecipes(long idProduct)
        {
            var sql = "select * from Recipe_Products where IdProduct = @Id";
            using (var conn = _dapperContext.CreateConnection())
            {
                var products = await conn.QueryAsync<ProductsRecipeModel>(sql, new { Id = idProduct });
                return products.ToList();
            }
        }
        public async Task<bool> DeleteProductRecipes(long id)
        {
            var sql = "DELETE FROM Recipe_Products WHERE IdProduct = @Id";

            using (var connection = _dapperContext.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                if (affectedRows > 0) return true;
                return false;
            }
        }
    }
}
