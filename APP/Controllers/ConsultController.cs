using APP.Models;
using APP.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APP.Controllers
{
    public class ConsultController : Controller
    {
        private IClientService _clientService;
        private IProductServices _productService;
        public ConsultController(IClientService clientService, IProductServices productService)
        {
            _clientService = clientService;
            _productService = productService;

        }
        // GET: ConsultController
        public async Task<ActionResult> ConsultClientIndex()
        {
            var client = await _clientService.FindAll();
            return View(client);
        }
        public async Task<ActionResult> ConsultDeleteClient(long id)
        {
            var status = await _clientService.DeleteClient(id);
            if (!status) return BadRequest(status);
              
            return RedirectToAction(nameof(ConsultClientIndex));
        }

        public async Task<ActionResult> ConsultProductsIndex()
        {
            var products = await _productService.FindAll();
            return View(products);
        }
    }
}
