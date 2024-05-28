using APP.Models;
using APP.Services.IService;
using APP.Utils;

namespace APP.Services
{
    public class ProductService : IProductServices
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/Product";
        public ProductService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<ProductModel>> FindAll()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<IEnumerable<ProductModel>>();
        }

        public async Task<ProductModel> FindProductById(long id)
        {
            var response = await _client.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<ProductModel>();
        }
        public async Task<ProductModel> CreateProduct(ProductModel prod)
        {
            var response = await _client.PostAsJson(BasePath,prod);
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<ProductModel>();
            }
            else
            {
                throw new Exception("Something went wrong");
            }

        }
        public async Task<ProductModel> UpdateProduct(ProductModel prod)
        {
            var response = await _client.PutAsJson(BasePath, prod);
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<ProductModel>();
            }
            else
            {
                throw new Exception("Something went wrong");
            }
        }
        public async Task<bool> DeleteProduct(long id)
        {
            var response = await _client.DeleteAsync($"{BasePath}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<bool>();
            }
            else
            {
                throw new Exception("Something went wrong");
            }
            
        }

        public async Task<IEnumerable<ProductModel>> FindProductByName(string name)
        {
            var response = await _client.GetAsync($"{BasePath}/search/{name}");
            var result = await response.ReadContentAs<IEnumerable<ProductModel>>();
            return result;
        }

        public async Task<IEnumerable<ProductsRecipeModel>> FindProductInRecipes(long idProduct)
        {
            var response = await _client.GetAsync($"{BasePath}/occurencies/{idProduct}");
            return await response.ReadContentAs<IEnumerable<ProductsRecipeModel>>();
        }

        public async Task<bool> DeleteProductRecipes(long id)
        {
            var response = await _client.DeleteAsync($"{BasePath}/product-recipes/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<bool>();
            }
            else
            {
                throw new Exception("Something went wrong");
            }
        }
    }
}
