using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Senhoritah.API.Context;
using Senhoritah.API.Model;

namespace Senhoritah.API.Repository
{
    public class ConfigRepository : IConfigRepository
    {
        private readonly context _context;
        public ConfigRepository(context context)
        {
           _context = context;
        }
        public async Task<ConfigModel> FindConfig()
        {
            ConfigModel config = await _context.Config.Where(c => c.id == 1).FirstOrDefaultAsync();

            return config;
        }

        public async Task<ConfigModel> UpdateConfig(ConfigModel config)
        {
            config.id = 1;
            _context.Config.Update(config);
            await _context.SaveChangesAsync();

            return config;
        }
    }
}
