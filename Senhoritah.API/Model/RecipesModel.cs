using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Senhoritah.API.Model
{
    public class RecipesModel
    {
        public long Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(300)]
        public string Description { get; set; } = string.Empty;
        [Column("decimal(18,2)")]
        public decimal Price { get; set; } = 0;
    }
}
