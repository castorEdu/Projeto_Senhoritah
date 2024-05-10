using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Senhoritah.API.Model
{
    public class ConfigModel
    {
        public int id { get; set; }
        [Display(Name = "Mão de Obra")]
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal mao_de_obra { get; set; }
        [Display(Name = "Energia, água")]
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal energia_agua { get; set; }
        [Display(Name = "Extra")]
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal extra { get; set; }
        [Display(Name = "Cálculo de Mercado")]
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal calculo_mercado { get; set; }
    }
}
