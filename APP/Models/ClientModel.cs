using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class ClientModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsSave { get; set; }
    }
}
