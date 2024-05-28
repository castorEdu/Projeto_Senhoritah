using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class BillsModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime InsertDate { get; set; } = DateTime.Now;
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Campo Inválido!")]
        public decimal Price { get; set; }
    }
}
