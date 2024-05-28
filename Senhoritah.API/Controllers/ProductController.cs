using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Senhoritah.API.Model;
using Senhoritah.API.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Senhoritah.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IProductRepository _respository;
        public ProductController(IProductRepository repository)
        {
            _respository = repository;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IEnumerable<ProductModel>> Get()
        {
            return await _respository.FindAll();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> Get(int id)
        {
            var prod = await _respository.FindProductById(id);
            if(prod == null) return NotFound();
            return Ok(prod);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<ProductModel>> Post(ProductModel prod)
        {
            if (prod == null) return BadRequest();
            var newProduct = await _respository.CreateProduct(prod);
            return Ok(newProduct);
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        public async Task<ActionResult<ProductModel>> Put(ProductModel prod)
        {
            if (prod == null) return BadRequest();
            var newProduct = await _respository.UpdateProduct(prod);
            return Ok(newProduct);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var status = await _respository.DeleteProduct(id);
            if(!status) return BadRequest();
            return true;
        }
        [HttpGet("search/{name}")]
        public async Task<ActionResult<ProductModel>> GetName(string name)
        {
            var product = await _respository.FindByName(name);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet("occurencies/{id}")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductInRecipes(long id)
        {
            var products = await _respository.FindProductInRecipes(id);
            if (products == null) return BadRequest();
            return Ok(products);
        }

        [HttpDelete("product-recipes/{id}")]
        public async Task<ActionResult<bool>> DeleteProductRecipe(int id)
        {
            var status = await _respository.DeleteProductRecipes(id);
            if (!status) return BadRequest();
            return true;
        }
    }
}
