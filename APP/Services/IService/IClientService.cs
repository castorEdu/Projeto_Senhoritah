using APP.Models;

namespace APP.Services.IService
{
    public interface IClientService
    {
        Task<IEnumerable<ClientModel>> FindAll();
        Task<ClientModel> FindClientById(long id);
        Task<ClientModel> CreateClient(ClientModel cli);
        Task<ClientModel> UpdateClient(ClientModel cli);
        Task<bool> DeleteClient(long id);
        Task<IEnumerable<ClientModel>> FindByName(string name);
    }
}
