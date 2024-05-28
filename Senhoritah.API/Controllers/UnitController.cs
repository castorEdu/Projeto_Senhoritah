using Microsoft.AspNetCore.Mvc;
using Senhoritah.API.Model;
using Senhoritah.API.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Senhoritah.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitRepository _unitRepository;
        public UnitController(IUnitRepository unit)
        {
            _unitRepository = unit;
        }
        [HttpGet]
        public async Task<IEnumerable<UnitsModel>> Get()
        {
            return await _unitRepository.FindAll();
        }
        [HttpGet("{Unit}")]
        public async Task<UnitsModel> GetName(string Unit)
        {
            return await _unitRepository.FindByName(Unit);
        }
        [HttpGet("search/{id}")]
        public async Task<UnitsModel> GetId(long id)
        {
            return await _unitRepository.FindById(id);
        }
    }
}
