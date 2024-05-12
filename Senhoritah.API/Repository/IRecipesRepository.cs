using Senhoritah.API.Model;

namespace Senhoritah.API.Repository
{
    public interface IRecipesRepository
    {
        Task<IEnumerable<RecipesModel>> FindAll();
        Task<RecipesModel> FindById(long id);
        Task<IEnumerable<RecipesModel>> FindByName(string name);
        Task<RecipesModel> Create(RecipesModel recipe);
        Task<RecipesModel> Update(RecipesModel recipe);
        Task<bool> Delete(long idRecipe);
        Task<IEnumerable<ProductsRecipeModel>> FindAllProductsByRecipe(long idRecipe);
        Task<ProductsRecipeModel> FindRecipeProductById(long idRecipe, long idProduct);
        Task<ProductsRecipeModel> CreateRecipeProduct(ProductsRecipeModel productRecipe);
        Task<ProductsRecipeModel> UpdateRecipeProduct(ProductsRecipeModel productRecipe);
        Task<bool> DeleteRecipeProduct(long idRecipe, long idProduct);
    }
}
