using APP.Models;

namespace APP.Models.ViewModel
{
    public class ProductViewModel
    {
        public ProductModel ProductModel { get; set; } = new ProductModel();
        public IEnumerable<ProductModel> Products { get; set; } = Enumerable.Empty<ProductModel>();
    }
}
