using Microsoft.AspNetCore.Mvc;
using Senhoritah.API.Model;
using Senhoritah.API.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Senhoritah.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        public IBillRepository _bill;
        public BillsController(IBillRepository bill)
        {
            _bill = bill;
        }

        [HttpGet]
        public async Task<IEnumerable<BillsModel>> Get()
        {
            return await _bill.FindAll();
        }

        [HttpPost]
        public async Task<ActionResult<BillsModel>> Post([FromBody] BillsModel bill)
        {
            if (bill == null) return BadRequest();
            return await _bill.Create(bill);
        }

        [HttpPut]
        public async Task<ActionResult<BillsModel>> Put([FromBody] BillsModel bill)
        {
            if (bill == null) return BadRequest();
            return await _bill.Update(bill);
        }

        // DELETE api/<BillsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var status = await _bill.Delete(id);
            if (!status) return BadRequest();
            return Ok(status);
        }

    }
}
