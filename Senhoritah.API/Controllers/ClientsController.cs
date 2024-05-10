using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Senhoritah.API.Model;
using Senhoritah.API.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Senhoritah.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private IClientsRepository _clientsRepository;
        public ClientsController(IClientsRepository clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }
        // GET: api/<ClientsController>
        [HttpGet]
        public async Task<IEnumerable<ClientsModel>> Get()
        {
            return await _clientsRepository.FindAll();
        }

        // GET api/<ClientsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientsModel>> Get(int id)
        {
            var client = await _clientsRepository.FindClientById(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        // POST api/<ClientsController>
        [HttpPost]
        public async Task<ActionResult<ClientsModel>> Post([FromBody] ClientsModel cli)
        {
            if(cli == null) return BadRequest();
            var newClient = await _clientsRepository.CreateClient(cli);
            return Ok(newClient);
        }

        // PUT api/<ClientsController>/5
        [HttpPut]
        public async Task<ActionResult<ClientsModel>> Put([FromBody] ClientsModel cli)
        {
            if(cli == null) return BadRequest();
            var client = await _clientsRepository.UpdateClient(cli);
            return Ok(client);
        }

        // DELETE api/<ClientsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var status = await _clientsRepository.DeleteClient(id);
            if (!status) return BadRequest();
            return Ok(status);
        }
        [HttpGet("search/{name}")]
        public async Task<ActionResult<ClientsModel>> GetName(string name)
        {
            var client = await _clientsRepository.FindByName(name);
            if (client == null) return NotFound();
            return Ok(client);
        }
    }
}
