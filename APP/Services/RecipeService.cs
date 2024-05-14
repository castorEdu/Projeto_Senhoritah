using APP.Models;
using APP.Services.IService;
using APP.Utils;
using System.Data;
using System.Runtime.InteropServices;

namespace APP.Services
{
    public class RecipeService : IRecipesService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/Recipes";
        public const string BasePathProducts = "api/Recipes/products";
        public RecipeService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<IEnumerable<RecipesModel>> FindAll()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<IEnumerable<RecipesModel>>();
        }

        public async Task<RecipesModel> FindById(long id)
        {
            var response = await _client.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<RecipesModel>();
        }

        public async Task<IEnumerable<RecipesModel>> FindByName(string name)
        {
            var response = await _client.GetAsync($"{BasePath}/search/{name}");
            return await response.ReadContentAs<IEnumerable<RecipesModel>>();
        }

        public async Task<RecipesModel> Create(RecipesModel recipe)
        {
            var response = await _client.PostAsJson(BasePath, recipe);
            return await response.ReadContentAs<RecipesModel>();
        }

        public async Task<RecipesModel> Update(RecipesModel recipe)
        {
            var response = await _client.PutAsJson(BasePath, recipe);
            return await response.ReadContentAs<RecipesModel>();
        }

        public async Task<bool> Delete(long idRecipe)
        {
            var response = await _client.DeleteAsync($"{BasePath}/{idRecipe}");
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<bool>();
            }
            else
            {
                throw new Exception("Something went wrong!");
            }
        }

        public async Task<IEnumerable<ProductsRecipeModel>> FindAllProductsByRecipe(long idRecipe)
        {
            var response = await _client.GetAsync($"{BasePathProducts}/{idRecipe}");
            return await response.ReadContentAs<IEnumerable<ProductsRecipeModel>>();
        }

        public async Task<ProductsRecipeModel> FindRecipeProductById(long idRecipe, long idProduct)
        {
            var response = await _client.GetAsync($"{BasePathProducts}/{idRecipe}/{idProduct}");
            return await response.ReadContentAs<ProductsRecipeModel>();
        }

        public async Task<ProductsRecipeModel> CreateRecipeProduct(ProductsRecipeModel productRecipe)
        {
            var response = await _client.PostAsJson(BasePathProducts, productRecipe);
            return await response.ReadContentAs<ProductsRecipeModel>();
        }

        public async Task<ProductsRecipeModel> UpdateRecipeProduct(ProductsRecipeModel productRecipe)
        {
            var response = await _client.PutAsJson(BasePathProducts, productRecipe);
            return await response.ReadContentAs<ProductsRecipeModel>();
        }

        public Task<bool> DeleteRecipeProduct(long idRecipe, long idProduct)
        {
            throw new NotImplementedException();
        }
    }
}
