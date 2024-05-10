using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class ConfigModel
    {
        public int id { get; set; }
        public decimal mao_de_obra { get; set; }
        public decimal energia_agua { get; set; }
        public decimal extra { get; set; }
        public decimal calculo_mercado { get; set; }
    }
}
