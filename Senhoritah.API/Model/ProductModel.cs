using System.ComponentModel.DataAnnotations;

namespace Senhoritah.API.Model
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Inválido!")]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo Inválido!")]
        [StringLength(200)]
        public string ProductCategory { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo Inválido!")]
        [StringLength(200)]
        public string Brand { get; set;} = string.Empty;
    }
}
