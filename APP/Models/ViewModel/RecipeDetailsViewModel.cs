namespace APP.Models.ViewModel
{
    public class RecipeDetailsViewModel
    {
        public RecipesModel Recipes { get; set; }
        public IEnumerable<ProductsRecipeModel> Products { get; set; }
    }
}
