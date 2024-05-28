using Microsoft.AspNetCore.Mvc.Rendering;

namespace APP.Models.ViewModel
{
    public class RecipeDetailsViewModel
    {
        public RecipesModel Recipes { get; set; }
        public IEnumerable<ProductsRecipeModel> Products { get; set; }
        public IEnumerable<ProductsRecipeModel> ProductsDeleted { get; set; } = Enumerable.Empty<ProductsRecipeModel>();
        public SelectList? Units { get; set; }
        public ProductsRecipeModel ProductsRecipe { get; set; } = new ProductsRecipeModel();
    }
}
