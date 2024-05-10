using APP.Models;
using APP.Services.IService;
using APP.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Xml.Linq;

namespace APP.Controllers
{
    public class ReleasesController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IProductServices _productService;
        private readonly IHttpContextAccessor _contextAccessor;

        public ReleasesController(IClientService clientService, IProductServices productServices, IHttpContextAccessor contextAccessor)
        {
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            _productService = productServices ?? throw new ArgumentNullException(nameof(productServices));
            _contextAccessor = contextAccessor; 
        }

        #region Client
        public IActionResult ReleaseIndex()
        {
            return View();
        }
        public async Task<ActionResult> ReleaseClients(ClientModel cli)
        {
            var clients = Enumerable.Empty<ClientModel>();
            if (string.IsNullOrWhiteSpace(cli.Name))
                clients = await _clientService.FindAll();
            else
                if(!cli.IsSave)
                    clients = await _clientService.FindByName(cli.Name);
                else clients = await _clientService.FindAll();
            var viewModel = new ClientViewModel
            {
                ClientModel = cli,
                Clients = clients
            };
            return View(viewModel);
        }
        public async Task<ActionResult> ReleaseSearchClient(string Name)
        {
            var cli = new ClientModel { Name = Name };
            return RedirectToAction("ReleaseClients", cli);
        }
        public async Task<ActionResult> ReleaseDeleteClient(long id)
        {
            var status = await _clientService.DeleteClient(id);
            if (!status) return BadRequest(status);

            return RedirectToAction(nameof(ReleaseClients));
        }
        public async Task<IActionResult> ReleaseCreateClient(ClientModel cli)
        {
            if (ModelState.IsValid)
            {
                var response = await _clientService.CreateClient(cli);
                if(response == null) return RedirectToAction(nameof(ReleaseClients));
                response.IsSave = true;
                return RedirectToAction("ReleaseClients", response);
            }
            return RedirectToAction(nameof(ReleaseClients));
        }
        #endregion

        #region Product
        

        #endregion

        public async Task<IActionResult> ReleaseRecipes()
        {
            
            return View();
        }

        public ActionResult GoBack()
        {
            return RedirectToAction("");
        }
    }
}
