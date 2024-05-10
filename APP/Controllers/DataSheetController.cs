using APP.Models;
using APP.Models.ViewModel;
using APP.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APP.Controllers
{
    public class DataSheetController : Controller
    {
        private readonly IProductServices _productService;
        private readonly IRecipesService _recipesService;
        public DataSheetController(IProductServices productServices, IRecipesService recipesService)
        {
            _productService = productServices;
            _recipesService = recipesService; 

        }
        public async Task<IActionResult> DataSheetIndex()
        {
            TempData.Clear();
            var response = await _recipesService.FindAll();
            return View(response);
        }
        public async Task<IActionResult> DataSheetCreate(DataSheetViewModel viewModel)
        {
            var view = new DataSheetViewModel
            {
                Products = await _productService.FindAll(),
                AddedProducts = GetAddedProducts()
            };
            return View(view);
        }
        public async Task<ActionResult> AddProduct(long id, string Unidade, decimal Quantidade)
        {
            if(Unidade == null || Quantidade == 0) return RedirectToAction(nameof(DataSheetCreate));
            var product = await _productService.FindProductById(id);
            if (product == null) return NotFound();
            string response = JsonConvert.SerializeObject(product);
            TempData[product.Id.ToString()] = response.ToString();
            return RedirectToAction(nameof(DataSheetCreate));
        }
        public List<ProductModel> GetAddedProducts()
        {
            var ProductsAdded = new List<ProductModel>();
            var list = TempData.ToList();
            foreach (var item in list)
            {
                var value = item.Value.ToString();
                var prod = JsonConvert.DeserializeObject<ProductModel>(value);
                ProductsAdded.Add(prod);
            }
            return ProductsAdded;
        }
        public IActionResult Cancel()
        {
            return RedirectToAction(nameof(DataSheetIndex));
        }
    }
}