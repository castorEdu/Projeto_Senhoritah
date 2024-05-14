using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Senhoritah.API.Model
{
    public class ProductsRecipeModel
    {
        public long Id { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        [ForeignKey("RecipeId")]
        public long RecipeId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; } = 0;
        [Required(ErrorMessage = "Campo Inválido!")]
        [StringLength(3)]
        public string? Unit { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo Inválido!")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Price { get; set; } = 0;
    }
}
