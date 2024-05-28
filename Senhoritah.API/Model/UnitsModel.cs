using Senhoritah.API.Model.Base;
using System.ComponentModel.DataAnnotations;

namespace Senhoritah.API.Model
{
    public class UnitsModel:BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(3)]
        public string Abbreviation { get; set; } = string.Empty;
    }
}
