using Senhoritah.API.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Senhoritah.API.Model
{
    public class BillsModel : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime InsertDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Campo Inválido!")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Preço")]
        public decimal Price { get; set; }
    }
}
