using APP.Models;
using APP.Services.IService;
using APP.Utils;

namespace APP.Services
{
    public class ConfigService : IConfigService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/Config";

        public ConfigService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<ConfigModel> Find()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<ConfigModel>();
        }

        public async Task<ConfigModel> Update(ConfigModel config)
        {
            var response = await _client.PutAsJson(BasePath, config);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ConfigModel>();
            else throw new Exception("Something went wrong with API");
        }
    }
}
