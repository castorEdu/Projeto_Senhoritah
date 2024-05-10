using Microsoft.AspNetCore.Mvc;
using Senhoritah.API.Model;
using Senhoritah.API.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Senhoritah.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private IConfigRepository _configRepository;
        public ConfigController(IConfigRepository configRepository)
        {
            _configRepository = configRepository ?? throw new ArgumentNullException(nameof(configRepository));
        }

        // GET: api/<ConfigController>
        [HttpGet]
        public async Task<ActionResult<ConfigModel>> Get()
        {
            var config = await _configRepository.FindConfig();
            return Ok(config);
        }
        // PUT api/<ConfigController>/5
        [HttpPut]
        public async Task<ActionResult<ConfigModel>> Put([FromBody] ConfigModel config)
        {
            if (config == null) return BadRequest();
            config.id = 1;
            await _configRepository.UpdateConfig(config);
            return Ok(config);
        }
    }
}
