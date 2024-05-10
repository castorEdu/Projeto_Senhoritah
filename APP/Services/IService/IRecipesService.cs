using APP.Models;

namespace APP.Services.IService
{
    public interface IRecipesService
    {
        Task<IEnumerable<RecipesModel>> FindAll();
    }
}
