using Microsoft.AspNetCore.Mvc;
using Senhoritah.API.Model;
using Senhoritah.API.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Senhoritah.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuysController : ControllerBase
    {
        private readonly IBuyRepository _buy;
        public BuysController(IBuyRepository buy)
        {
            _buy = buy;
        }

        [HttpGet]
        public async Task<IEnumerable<BuyModel>> Get()
        {
            var buys = await _buy.FindAll();
            return buys;
        }

        [HttpPost]
        public async Task<BuyModel> Post([FromBody] BuyModel model)
        {
            var buy = await _buy.Create(model);
            return buy;

        }

        [HttpPut]
        public async Task<BuyModel> Put([FromBody] BuyModel model)
        {
            var buy = await _buy.Update(model);
            return buy;
        }
    }
}
