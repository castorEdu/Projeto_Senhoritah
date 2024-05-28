using Senhoritah.API.Model;

namespace Senhoritah.API.Repository
{
    public interface IUnitRepository
    {
        Task<IEnumerable<UnitsModel>> FindAll();
        Task<UnitsModel> FindByName(string Unit);
        Task<UnitsModel> FindById(long id);
    }
}
