using APP.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace APP.Controllers
{
    public class ConfigController : Controller
    {
        private readonly IConfigService _configService;
        public ConfigController(IConfigService configService)
        {
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
        }
        public async Task<IActionResult> ConfigIndex()
        {
            var config = await _configService.Find();
            return View(config);
        }
    }
}
