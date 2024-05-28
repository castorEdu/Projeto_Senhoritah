using Senhoritah.API.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Senhoritah.API.Model
{
    public class ProductModel:BaseEntity
    {
        [Required(ErrorMessage = "Campo Inválido!")]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo Inválido!")]
        [StringLength(200)]
        public string ProductCategory { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo Inválido!")]
        [StringLength(200)]
        public string Brand { get; set;} = string.Empty;
        public long IdUnit { get; set; }
        [ForeignKey("IdUnit")]
        public virtual UnitsModel? Unit { get; set; }
    }
}
