using APP.Models;
using APP.Services.IService;
using APP.Utils;

namespace APP.Services
{
    public class ClientService : IClientService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/Clients";
        public ClientService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<IEnumerable<ClientModel>> FindAll()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<IEnumerable<ClientModel>>();
        }

        public async Task<ClientModel> FindClientById(long id)
        {
            var response = await _client.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<ClientModel>();
        }
        public async Task<ClientModel> CreateClient(ClientModel cli)
        {
            var response = await _client.PostAsJson(BasePath,cli);
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<ClientModel>();
            }
            else
            {
                throw new Exception("Something went wrong");
            }
        }
        public async Task<ClientModel> UpdateClient(ClientModel cli)
        {
            var response = await _client.PutAsJson(BasePath, cli);
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<ClientModel>();
            }
            else
            {
                throw new Exception("Something went wrong");
            }
        }

        public async Task<bool> DeleteClient(long id)
        {
            var response = await _client.DeleteAsync($"{BasePath}/{id}");
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();
            else throw new Exception("Something went wrong when calling API");
        }
        public async Task<IEnumerable<ClientModel>> FindByName(string name)
        {
            var response = await _client.GetAsync($"{BasePath}/search/{name}");
            return await response.ReadContentAs<IEnumerable<ClientModel>>();
        }

    }
}
