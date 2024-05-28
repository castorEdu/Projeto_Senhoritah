using APP.Models;
using Microsoft.AspNetCore.Mvc;

namespace APP.Services.IService
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductModel>> FindAll();
        Task<ProductModel> FindProductById(long id);
        Task<IEnumerable<ProductModel>> FindProductByName(string name);
        Task<IEnumerable<ProductsRecipeModel>> FindProductInRecipes(long idProduct);
        Task<ProductModel> CreateProduct(ProductModel prod);
        Task<ProductModel> UpdateProduct(ProductModel prod);
        Task<bool> DeleteProduct(long id);
        Task<bool> DeleteProductRecipes(long id);
    }
}
