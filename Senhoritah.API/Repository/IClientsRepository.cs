using Senhoritah.API.Model;

namespace Senhoritah.API.Repository
{
    public interface IClientsRepository
    {
        Task<IEnumerable<ClientsModel>> FindAll();
        Task<ClientsModel> FindClientById(long id);
        Task<IEnumerable<ClientsModel>> FindByName(string name);
        Task<ClientsModel> CreateClient(ClientsModel cli);
        Task<ClientsModel> UpdateClient(ClientsModel cli);
        Task<bool> DeleteClient(long id);
    }
}
