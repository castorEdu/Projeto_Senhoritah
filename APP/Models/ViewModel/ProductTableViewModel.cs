using Microsoft.AspNetCore.Mvc.Rendering;

namespace APP.Models.ViewModel
{
    public class ProductTableViewModel
    {
        public IEnumerable<ProductModel> Products { get; set; } = Enumerable.Empty<ProductModel>();
        public ProductsRecipeModel ProductsRecipe { get; set; } = new ProductsRecipeModel();
        public SelectList? Units { get; set; }
    }
}
