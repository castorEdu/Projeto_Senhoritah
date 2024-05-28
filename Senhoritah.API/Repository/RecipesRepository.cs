using Microsoft.EntityFrameworkCore;
using Senhoritah.API.Context;
using Senhoritah.API.Model;
using Dapper;

namespace Senhoritah.API.Repository
{
    public class RecipesRepository : IRecipesRepository
    {
        private readonly context _context;
        private readonly DapperContext _dapperContext;
        public RecipesRepository(context context, DapperContext dapperContext)
        {
            _context = context;
            _dapperContext = dapperContext;

        }
        #region Recipes
        public async Task<IEnumerable<RecipesModel>> FindAll()
        {
            var sql = "SELECT * FROM Recipes";
            using (var conn = _dapperContext.CreateConnection())
            {
                var recipes = await conn.QueryAsync<RecipesModel>(sql);
                return recipes.ToList();
            }

        }
        public async Task<RecipesModel> FindById(long id)
        {
            var sql = "SELECT * FROM Recipes Where id = @Id";
            using (var conn = _dapperContext.CreateConnection())
            {
                var recipes = await conn.QueryAsync<RecipesModel>(sql, new { id });
                return recipes.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<RecipesModel>> FindByName(string name)
        {
            var sql = "SELECT * FROM Recipes WHERE Name LIKE @Name";
            using (var conn = _dapperContext.CreateConnection())
            {
                var recipes = await conn.QueryAsync<RecipesModel>(sql, new {Name = $"%{name}%"});
                return recipes.ToList();
            }
        }
        public async Task<RecipesModel> Create(RecipesModel recipe)
        {
            var sqlInsert = "INSERT INTO Recipes (Name, Description, Price) VALUES (@Name, @Description, @Price); SELECT CAST(SCOPE_IDENTITY() as int);";
            using (var conn = _dapperContext.CreateConnection())
            {
                var newRecipeId = await conn.QuerySingleAsync<int>(sqlInsert, new { recipe.Name, recipe.Description, recipe.Price });

                var sqlSelect = "SELECT * FROM Recipes WHERE Id = @Id";
                var recipeCreated = await conn.QuerySingleAsync<RecipesModel>(sqlSelect, new { Id = newRecipeId });

                return recipeCreated;
            }
        }
        public async Task<RecipesModel> Update(RecipesModel recipe)
        {
            var sqlUpdate = "UPDATE Recipes SET Name = @Name, Description = @Description, Price = @Price WHERE Id = @id";
            using (var conn = _dapperContext.CreateConnection())
            {
                await conn.ExecuteAsync(sqlUpdate, new { recipe.Name, recipe.Description, recipe.Price, recipe.Id });

                var sqlSelect = "SELECT * FROM Recipes WHERE id = @Id";
                var recipeUpdated = await conn.QueryAsync<RecipesModel>(sqlSelect, new { recipe.Id });

                return recipeUpdated.FirstOrDefault();
            }
        }
        public async Task<bool> Delete(long idRecipe)
        {
            var sql = "DELETE FROM Recipes WHERE Id = @Id";
            var sqlProducts = "DELETE FROM Recipe_Products WHERE Id = @Id";

            using (var connection = _dapperContext.CreateConnection())
            {
                var affectedRowsProducts = await connection.ExecuteAsync(sqlProducts, new { Id = idRecipe });
                if(affectedRowsProducts > 0)
                {
                    var affectedRows = await connection.ExecuteAsync(sql, new { Id = idRecipe });
                    if (affectedRows > 0) return true;
                    return false;
                }
                return false;
            }
        }
        #endregion

        #region RecipeProduct
        public async Task<IEnumerable<ProductsRecipeModel>> FindAllProductsByRecipe(long idRecipe)
        {
            var sql = "SELECT * FROM Recipe_Products WHERE IdRecipe = @Id";
            using (var conn = _dapperContext.CreateConnection())
            {
                var RecipeProducts = await conn.QueryAsync<ProductsRecipeModel>(sql, new { Id = idRecipe });

                return RecipeProducts.ToList();
            }
        }
        public async Task<ProductsRecipeModel> FindRecipeProductById(long idRecipe, long idProduct)
        {
            var sql = "SELECT * FROM Recipe_Products WHERE IdRecipe = @IdRecipe and IdProduct = @IdProduct";
            using (var conn = _dapperContext.CreateConnection())
            {
                var RecipeProducts = await conn.QueryAsync<ProductsRecipeModel>(sql, new { IdRecipe = idRecipe, IdProduct = idProduct });

                return RecipeProducts.FirstOrDefault();
            }
        }
        public async Task<ProductsRecipeModel> CreateRecipeProduct(ProductsRecipeModel productRecipe)
        {
            var sqlInsert = "INSERT INTO Recipe_Products (IdProduct, IdRecipe, IdUnit, Weight, Amount, Price) VALUES (@IdProduct, @IdRecipe, @IdUnit, @Weight, @Amount, @Price); SELECT CAST(SCOPE_IDENTITY() as int);";
            using (var conn = _dapperContext.CreateConnection())
            {
                var newRecipeProductId = await conn.QuerySingleAsync<int>(sqlInsert, new { IdProduct = productRecipe.IdProduct, IdRecipe = productRecipe.IdRecipe, IdUnit = productRecipe.IdUnit, Weight = productRecipe.Weight, Amount = productRecipe.Amount, Price =productRecipe.Price });

                var sqlSelect = "SELECT * FROM Recipe_Products WHERE Id = @Id";
                var recipeProductCreated = await conn.QuerySingleAsync<ProductsRecipeModel>(sqlSelect, new { Id = newRecipeProductId });

                return recipeProductCreated;
            }
        }
        public async Task<ProductsRecipeModel> UpdateRecipeProduct(ProductsRecipeModel productRecipe)
        {
            var sqlUpdate = "UPDATE Recipe_Products SET IdProduct = @IdProduct, IdRecipe = @IdRecipe, IdUnit = @IdUnit, Weight = @Weight,Amount = @Amount, Price = @Price  WHERE Id = @id";
            using (var conn = _dapperContext.CreateConnection())
            {
                await conn.ExecuteAsync(sqlUpdate, new { productRecipe.IdProduct, productRecipe.IdRecipe, productRecipe.IdUnit, productRecipe.Weight, productRecipe.Amount, productRecipe.Price, productRecipe.Id });

                var sqlSelect = "SELECT * FROM Recipe_Products WHERE id = @Id";
                var recipeUpdated = await conn.QueryAsync<ProductsRecipeModel>(sqlSelect, new { productRecipe.Id });

                return recipeUpdated.FirstOrDefault();
            }
        }
        public async Task<bool> DeleteRecipeProduct(long idRecipe, long idProduct)
        {
            var sql = "DELETE FROM Recipe_Products WHERE IdRecipe = @IdRecipe AND IdProduct = @IdProduct";

            using (var connection = _dapperContext.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(sql, new { IdRecipe = idRecipe, IdProduct = idProduct });
                if (affectedRows > 0) return true;
                return false;
            }
        }
        #endregion
    }
}
