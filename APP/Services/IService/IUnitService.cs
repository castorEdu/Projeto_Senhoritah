using APP.Models;

namespace APP.Services.IService
{
    public interface IUnitService
    {
        Task<IEnumerable<UnitModel>> FindAll();
        Task<UnitModel> FindByName(string Unit);
        Task<UnitModel> FindById(long Id);
    }
}
