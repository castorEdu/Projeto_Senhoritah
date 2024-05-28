using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Senhoritah.API.Model.Base;
using System.ComponentModel.DataAnnotations;

namespace Senhoritah.API.Model
{
    public class ClientsModel:BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty ;
        [Required]
        [StringLength(11)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
