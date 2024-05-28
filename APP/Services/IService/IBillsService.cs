using APP.Models;

namespace APP.Services.IService
{
    public interface IBillsService
    {
        Task<IEnumerable<BillsModel>> FindAll();
        Task<BillsModel> Create(BillsModel bill);
        Task<BillsModel> Update(BillsModel bill);
        Task<bool> Delete(long id);
    }
}
