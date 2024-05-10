using APP.Models;

namespace APP.Services.IService
{
    public interface IConfigService
    {
        Task<ConfigModel> Find();
        Task<ConfigModel> Update(ConfigModel config);
    }
}
