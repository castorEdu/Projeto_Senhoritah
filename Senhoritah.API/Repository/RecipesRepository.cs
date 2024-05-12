using Microsoft.EntityFrameworkCore;
using Senhoritah.API.Context;
using Senhoritah.API.Model;

namespace Senhoritah.API.Repository
{
    public class RecipesRepository : IRecipesRepository
    {
        private readonly context _context;
        public RecipesRepository(context context)
        {
            _context = context;
        }
        #region Recipes
        public async Task<IEnumerable<RecipesModel>> FindAll()
        {
            return await _context.Recipes.ToListAsync();
        }
        public async Task<RecipesModel> FindById(long id)
        {
            var recipes = await _context.Recipes.Where(p => p.Id == id).FirstOrDefaultAsync();
            return recipes;
        }

        public async Task<IEnumerable<RecipesModel>> FindByName(string name)
        {
            var recipes = await _context.Recipes.Where(p => p.Name.Contains(name)).ToListAsync();
            return recipes;
        }
        public async Task<RecipesModel> Create(RecipesModel recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }
        public async Task<RecipesModel> Update(RecipesModel recipe)
        {
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }
        public async Task<bool> Delete(long idRecipe)
        {
            try
            {
                var recipeProducts = await _context.Recipe_products.Where(p => p.RecipeId == idRecipe).ToListAsync();
                RecipesModel recipe = await _context.Recipes.Where(p => p.Id == idRecipe).FirstOrDefaultAsync();
                if (recipe == null) return false;
                _context.Recipes.Remove(recipe);

                foreach (var i in recipeProducts)
                {
                    var status = DeleteRecipeProduct(idRecipe, i.ProductId);
                    if(!status.Result) return false;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region RecipeProduct
        public async Task<IEnumerable<ProductsRecipeModel>> FindAllProductsByRecipe(long idRecipe)
        {
            var recipeProducts = await _context.Recipe_products.Where(p => p.RecipeId == idRecipe).ToListAsync();
            return recipeProducts;
        }
        public async Task<ProductsRecipeModel> FindRecipeProductById(long idRecipe, long idProduct)
        {
            var recipeProducts = await _context.Recipe_products.Where(p => p.RecipeId == idRecipe && p.ProductId == idProduct).FirstOrDefaultAsync();

            return recipeProducts;
        }
        public async Task<ProductsRecipeModel> CreateRecipeProduct(ProductsRecipeModel productRecipe)
        {
            _context.Recipe_products.Add(productRecipe);
            await _context.SaveChangesAsync();

            return productRecipe;
        }
        public async Task<ProductsRecipeModel> UpdateRecipeProduct(ProductsRecipeModel productRecipe)
        {
            _context.Recipe_products.Update(productRecipe);
            await _context.SaveChangesAsync();

            return productRecipe;
        }
        public async Task<bool> DeleteRecipeProduct(long idRecipe, long idProduct)
        {
            try
            {
                ProductsRecipeModel recipeProduct = await _context.Recipe_products.Where(p => p.RecipeId == idRecipe && p.ProductId == idProduct).FirstOrDefaultAsync();
                if (recipeProduct == null) return false;
                _context.Recipe_products.Remove(recipeProduct);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
