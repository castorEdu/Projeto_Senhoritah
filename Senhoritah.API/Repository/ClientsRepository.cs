using Microsoft.EntityFrameworkCore;
using Senhoritah.API.Context;
using Senhoritah.API.Model;

namespace Senhoritah.API.Repository
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly context _context;
        public ClientsRepository(context context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ClientsModel>> FindAll()
        {
            return await _context.Clients.ToListAsync(); 
        }

        public async Task<ClientsModel> FindClientById(long id)
        {
            var client = await _context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();
            return client;
        }
        public async Task<ClientsModel> CreateClient(ClientsModel cli)
        {
            _context.Clients.Add(cli);
            await _context.SaveChangesAsync();
            return cli;
        }
        public async Task<ClientsModel> UpdateClient(ClientsModel cli)
        {
            _context.Clients.Update(cli);
            await _context.SaveChangesAsync();
            return cli;
        }

        public async Task<bool> DeleteClient(long id)
        {
            try
            {
                ClientsModel client = await _context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();
                if (client == null) return false;
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ClientsModel>> FindByName(string name)
        {
            return await _context.Clients.Where(c => c.Name.Contains(name)).ToListAsync();
        }
    }
}
