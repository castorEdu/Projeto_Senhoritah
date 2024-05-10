using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Senhoritah.API.Model
{
    public class ProductsRecipeModel
    {
        public long Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual ProductModel? Product { get; set; }
        public long RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public virtual RecipesModel? Recipes { get; set; }
        public decimal? Amount { get; set; }
        [Required]
        [StringLength(2)]
        public string? Unit { get; set;}
    }
}
