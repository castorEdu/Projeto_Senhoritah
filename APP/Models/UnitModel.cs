using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class UnitModel
    {
        public long Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(3)]
        public string Abbreviation { get; set; } = string.Empty;
    }
}
