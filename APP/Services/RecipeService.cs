using APP.Models;
using APP.Services.IService;
using APP.Utils;

namespace APP.Services
{
    public class RecipeService : IRecipesService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/Recipes";
        public RecipeService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<IEnumerable<RecipesModel>> FindAll()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<IEnumerable<RecipesModel>>();
        }
    }
}
