using Senhoritah.API.Model;

namespace Senhoritah.API.Repository
{
    public interface IBillRepository
    {
        Task<IEnumerable<BillsModel>> FindAll();
        Task<BillsModel> Create(BillsModel bill);
        Task<BillsModel> Update(BillsModel bill);
        Task<bool> Delete(long id);
    }
}
