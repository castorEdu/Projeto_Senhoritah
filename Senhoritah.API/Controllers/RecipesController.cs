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
        // GET: api/<RecipesController>
        [HttpGet]
        public async Task<IEnumerable<RecipesModel>> Get()
        {
            return await _recipesRepository.FindAll();
        }

        // GET api/<RecipesController>/5
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

        // DELETE api/<RecipesController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _recipesRepository.Delete(id);
        }

    }
}
