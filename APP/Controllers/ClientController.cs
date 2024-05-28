using APP.Models;
using APP.Models.ViewModel;
using APP.Services;
using APP.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace APP.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        public async Task<ActionResult> ClientIndex(ClientModel cli)
        {
            var client = await _clientService.FindAll();
            var viewModel = new ClientViewModel
            {
                Clients = client
            };
            return View(viewModel);
        }
        public async Task<ActionResult> SearchClient(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var client = await _clientService.FindByName(Name);
                if (client == null) client = Enumerable.Empty<ClientModel>();
                return Ok(client);
            }
            else
            {
                var allClients = await _clientService.FindAll();
                if(allClients == null) allClients = Enumerable.Empty<ClientModel>();
                return Ok(allClients);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientViewModel client)
        {
            if (ModelState.IsValid)
            {
                var response = await _clientService.CreateClient(client.ClientModel);
                if (response == null) return RedirectToAction(nameof(ClientIndex));
                response.IsSave = true;
                return RedirectToAction(nameof(ClientIndex));
            }
            return RedirectToAction(nameof(ClientIndex));
        }
        [HttpPost]
        public async Task<ActionResult> DeleteClient(long id)
        {
            var status = await _clientService.DeleteClient(id);
            return RedirectToAction(nameof(ClientIndex));
        }
        public async Task<ActionResult> EditClient(ClientModel client)
        {
            var response = await _clientService.UpdateClient(client);
            return RedirectToAction(nameof(ClientIndex));
        }
        public async Task<ActionResult> ClientDetails(long id)
        {
            var SelectedClient = await _clientService.FindClientById(id);
            if (SelectedClient == null) return BadRequest();
            return View(SelectedClient);
        }
    }
}
