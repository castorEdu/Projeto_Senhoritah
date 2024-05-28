using APP.Models;
using APP.Services.IService;
using APP.Utils;

namespace APP.Services
{
    public class BillsService : IBillsService
    {
        private HttpClient _client;
        private readonly string BasePath = "api/Bills";

        public BillsService(HttpClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<BillsModel>> FindAll()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<IEnumerable<BillsModel>>();
        }

        public async Task<BillsModel> Create(BillsModel bill)
        {
            var response = await _client.PostAsJson(BasePath,bill);
            return await response.ReadContentAs<BillsModel>();
        }
        public async Task<BillsModel> Update(BillsModel bill)
        {
            var response = await _client.PutAsJson(BasePath, bill);
            return await response.ReadContentAs<BillsModel>();
        }

        public async Task<bool> Delete(long id)
        {
            var response = await _client.DeleteAsync($"{BasePath}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<bool>();
            }
            else
            {
                throw new Exception("Something went wrong!");
            }
        }
    }
}
