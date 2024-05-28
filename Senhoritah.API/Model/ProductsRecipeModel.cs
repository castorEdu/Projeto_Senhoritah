using Senhoritah.API.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Senhoritah.API.Model
{
    [Table("Recipe_products")]
    public class ProductsRecipeModel:BaseEntity
    {
        public long IdProduct { get; set; }
        public long IdRecipe { get; set; }
        public decimal Amount { get; set; } = 0;
        public decimal Weight { get; set; } = 0;
        public long IdUnit { get; set; }
        public decimal? Price { get; set; } = 0;
    }
}
