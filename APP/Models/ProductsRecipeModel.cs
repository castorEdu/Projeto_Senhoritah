using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class ProductsRecipeModel
    {
        public long Id { get; set; }
        public int ProductId { get; set; }
        public virtual ProductModel? Product { get; set; }
        public long RecipeId { get; set; }
        public virtual RecipesModel? Recipes { get; set; }
        public decimal? Amount { get; set; }
        public string? Unit { get; set; }
    }
}
