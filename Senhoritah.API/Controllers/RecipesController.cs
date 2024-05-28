using Microsoft.AspNetCore.Mvc;
using Senhoritah.API.Model;
using Senhoritah.API.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Senhoritah.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        public IRecipesRepository _recipesRepository;
        public RecipesController(IRecipesRepository recipesRepository)
        {
            _recipesRepository = recipesRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<RecipesModel>> Get()
        {
            return await _recipesRepository.FindAll();
        }
        [HttpGet("{id}")]
        public async Task<RecipesModel> GetById(int id)
        {
            return await _recipesRepository.FindById(id);
        }
        [HttpGet("search/{name}")]
        public async Task<IEnumerable<RecipesModel>> GetByName(string name)
        {
            return await _recipesRepository.FindByName(name);
        }
        [HttpPost]
        public async Task<RecipesModel> Create(RecipesModel recipe)
        {
            return await _recipesRepository.Create(recipe);
        }
        [HttpPut]
        public async Task<RecipesModel> Update(RecipesModel recipe)
        {
            return await _recipesRepository.Update(recipe);
        }
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _recipesRepository.Delete(id);
        }

        [HttpGet("products/{id}")]
        public async Task<IEnumerable<ProductsRecipeModel>> GetProducts(long id)
        {
            return await _recipesRepository.FindAllProductsByRecipe(id);
        }
        [HttpGet("products/{idRecipe}/{idProduct}")]
        public async Task<ProductsRecipeModel> GetProducts(long idRecipe, long idProduct)
        {
            return await _recipesRepository.FindRecipeProductById(idRecipe, idProduct);
        }
        [HttpPost("products")]
        public async Task<ProductsRecipeModel> Create(ProductsRecipeModel prod)
        {
            return await _recipesRepository.CreateRecipeProduct(prod);
        }
        [HttpPut("products")]
        public async Task<ProductsRecipeModel> Update(ProductsRecipeModel prod)
        {
            return await _recipesRepository.UpdateRecipeProduct(prod);
        }
        [HttpDelete("products/{idRecipe}/{idProduct}")]
        public async Task<bool> Delete(long idRecipe, long idProduct)
        {
            return await _recipesRepository.DeleteRecipeProduct(idRecipe, idProduct);
        }

    }
}
