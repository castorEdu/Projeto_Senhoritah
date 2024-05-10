using Senhoritah.API.Model;

namespace Senhoritah.API.Repository
{
    public interface IConfigRepository
    {
        Task<ConfigModel> FindConfig();
        Task<ConfigModel> UpdateConfig(ConfigModel config);
    }
}

