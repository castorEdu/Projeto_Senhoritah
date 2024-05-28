namespace APP.Models
{
    public class ProductsRecipeModel
    {
        public long Id { get; set; }
        public long IdProduct { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public ProductModel Product { get; set; } = new ProductModel();
        public long IdRecipe { get; set; }
        public decimal Amount { get; set; }
        public decimal Weight { get; set; } = 0;
        public string? Unit { get; set; }
        public long IdUnit { get; set; }
        public decimal? Price { get; set; } = 0;
        public int Index { get; set; } = 0;
    }
}
