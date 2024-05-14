namespace APP.Models.ViewModel
{
    public class PreSaveDetailsViewModel
    {
        public string Name { get; set; } = string.Empty;
        public IEnumerable<ProductsRecipeModel> products { get; set; } = Enumerable.Empty<ProductsRecipeModel>();
    }
}
