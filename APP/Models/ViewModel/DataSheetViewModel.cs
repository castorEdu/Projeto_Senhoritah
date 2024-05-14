namespace APP.Models.ViewModel
{
    public class DataSheetViewModel
    {
        public RecipesModel Recipe { get; set; } = new RecipesModel();
        public IEnumerable<ProductModel> Products { get; set; } = Enumerable.Empty<ProductModel>();
        public IEnumerable<ProductsRecipeModel> AddedProducts { get; set; } = Enumerable.Empty<ProductsRecipeModel>();
        public IEnumerable<RecipesModel> Recipes { get; set; } = Enumerable.Empty<RecipesModel>();
    }
}
