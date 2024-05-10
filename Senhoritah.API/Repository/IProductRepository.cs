using Senhoritah.API.Model;

namespace Senhoritah.API.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> FindAll();
        Task<ProductModel> FindProductById(long id);
        Task<IEnumerable<ProductModel>> FindByName(string name);
        Task<ProductModel> CreateProduct(ProductModel prod);
        Task<ProductModel> UpdateProduct(ProductModel prod);
        Task<bool> DeleteProduct(long id);
    }
}
