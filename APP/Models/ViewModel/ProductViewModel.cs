using APP.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace APP.Models.ViewModel
{
    public class ProductViewModel
    {
        public ProductModel ProductModel { get; set; } = new ProductModel();
        public IEnumerable<ProductModel> Products { get; set; } = Enumerable.Empty<ProductModel>();
        public SelectList? Units { get; set; } = new SelectList("");
        [Required(ErrorMessage = "Campo Inválido!")]
        public string Unit { get; set; } = string.Empty;
    }
}
