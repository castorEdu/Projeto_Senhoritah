using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Models
{
    public class ProductModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Campo Inválido!")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo Inválido!")]
        public string ProductCategory { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo Inválido!")]
        public string Brand { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo Inválido!")]
        public long IdUnit { get; set; }
        public virtual UnitModel? Unit { get; set; }
        public bool IsSave { get; set; }
    }
}
