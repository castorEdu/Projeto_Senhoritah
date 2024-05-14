using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class ProductsRecipeModel
    {
        public long Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public long RecipeId { get; set; }
        public decimal? Amount { get; set; }
        public string? Unit { get; set; }
        public decimal? Price { get; set; } = 0;
        public int Index { get; set; } = 0;
    }
}
