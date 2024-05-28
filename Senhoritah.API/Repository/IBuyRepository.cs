using Senhoritah.API.Model;

namespace Senhoritah.API.Repository
{
    public interface IBuyRepository
    {
        Task<IEnumerable<BuyModel>> FindAll();
        Task<BuyModel> Create(BuyModel buy);
        Task<BuyModel> Update(BuyModel buy);
    }
}
