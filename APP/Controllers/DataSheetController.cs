using APP.Models;
using APP.Models.ViewModel;
using APP.Services.IService;
using APP.Utils;
using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APP.Controllers
{
    public class DataSheetController : Controller
    {
        private readonly IProductServices _productService;
        private readonly IRecipesService _recipesService;
        private Resorces _resorces;
        public DataSheetController(IProductServices productServices, IRecipesService recipesService, Resorces resorces)
        {
            _productService = productServices;
            _recipesService = recipesService;
            _resorces = resorces;
        }
        public async Task<IActionResult> DataSheetIndex()
        {
            //TempData.Clear();
            _resorces.ClearAll();
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
        public ActionResult DataSheetName(string Name)
        {
            if (string.IsNullOrEmpty(Name)) return RedirectToAction(nameof(DataSheetCreate));
            _resorces.nameRecipe = Name;
            return RedirectToAction(nameof(DataSheetProductSession));
        }
        public async Task<IActionResult> DataSheetProductSession() 
        {
            var view = new DataSheetViewModel
            {
                Products = await _productService.FindAll(),
                AddedProducts = GetAddedProducts()
            };
            return View(view);
        }
        public async Task<ActionResult> Search(string name)
        {
            var searchResult = await _productService.FindProductByName(name);
            if(searchResult == null) searchResult = Enumerable.Empty<ProductModel>();

            var viewModel = new DataSheetViewModel
            {
                Products = searchResult
            };
            return View(viewModel);
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
        public ActionResult ChangeName()
        {
            _resorces.name = "Eduardo";
            return RedirectToAction(nameof(DataSheetCreate));
        }
        public ActionResult GetName() 
        {
            var nome = _resorces.name;
            return RedirectToAction(nameof(DataSheetCreate));
        } 
        public IActionResult Cancel()
        {
            return RedirectToAction(nameof(DataSheetIndex));
        }
    }
}