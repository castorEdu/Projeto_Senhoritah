using APP.Models;
using APP.Services.IService;
using APP.Utils;

namespace APP.Services
{
    public class UnitService : IUnitService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/Unit";
        public UnitService(HttpClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<UnitModel>> FindAll()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<IEnumerable<UnitModel>>();
        }
        public async Task<UnitModel> FindByName(string Unit)
        {
            var response = await _client.GetAsync($"{BasePath}/{Unit}");
            return await response.ReadContentAs<UnitModel>();
        }

        public async Task<UnitModel> FindById(long Id)
        {
            var response = await _client.GetAsync($"{BasePath}/search/{Id}");
            return await response.ReadContentAs<UnitModel>();
        }
    }
}
