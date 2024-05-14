using APP.Models;
using APP.Models.ViewModel;
using APP.Services.IService;
using APP.Utils;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> DataSheetCreate()
        {
            var view = new DataSheetViewModel
            {
                Products = await _productService.FindAll()
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
                AddedProducts = _resorces.productsAdded
            };
            ViewData["Name"] = _resorces.nameRecipe;
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
            if(Unidade == null || Quantidade == 0) return RedirectToAction(nameof(DataSheetProductSession));
            var prod = new ProductsRecipeModel();
            var product = await _productService.FindProductById(id);
            prod.ProductId = product.Id;
            prod.ProductName = product.Name;
            prod.Unit = Unidade;
            prod.Amount = Quantidade;
            prod.Index = _resorces.SetIndex();
            _resorces.AddProductToList(prod);
            return RedirectToAction(nameof(DataSheetProductSession));
        }
        public ActionResult RemoveAddedProducts(int Index)
        {
            _resorces.RemoveProductFromList(Index);
            return RedirectToAction(nameof(DataSheetProductSession));
        }
        //public async Task<ActionResult> AddProduct(long id, string Unidade, decimal Quantidade)
        //{
        //    if(Unidade == null || Quantidade == 0) return RedirectToAction(nameof(DataSheetCreate));
        //    var product = await _productService.FindProductById(id);
        //    if (product == null) return NotFound();
        //    string response = JsonConvert.SerializeObject(product);
        //    TempData[product.Id.ToString()] = response.ToString();
        //    return RedirectToAction(nameof(DataSheetCreate));
        //}
        //public List<ProductModel> GetAddedProducts()
        //{
        //    var ProductsAdded = new List<ProductModel>();
        //    var list = TempData.ToList();
        //    foreach (var item in list)
        //    {
        //        var value = item.Value.ToString();
        //        var prod = JsonConvert.DeserializeObject<ProductModel>(value);
        //        ProductsAdded.Add(prod);
        //    }
        //    return ProductsAdded;
        //}
        public IActionResult Cancel()
        {
            _resorces.ClearAll();
            return RedirectToAction(nameof(DataSheetIndex));
        }
        public ActionResult PreSave(string Description)
        {
            if (!string.IsNullOrEmpty(Description)) _resorces.descriptionRecipe = Description;
            var details = new PreSaveDetailsViewModel
            {
                Name = _resorces.nameRecipe,
                products = _resorces.productsAdded
            };
            return View(details);
        }
        public async Task<ActionResult> Save()
        {
            var recipe = new RecipesModel
            {
                Name = _resorces.nameRecipe,
                Description = _resorces.descriptionRecipe,
                Price = 10
            };
            var newRecipe = await _recipesService.Create(recipe);
            foreach (var item in _resorces.productsAdded)
            {
                item.RecipeId = newRecipe.Id;
                item.Price = 10;
                await _recipesService.CreateRecipeProduct(item);
            }
            return RedirectToAction(nameof(DataSheetIndex));
        }
        [HttpPost]
        public async Task<ActionResult> DeleteRecipes(long id)
        {
            var status = await _recipesService.Delete(id);
            return RedirectToAction(nameof(DataSheetIndex));
        }
        public async Task<ActionResult> RecipeDetails(long id)
        {
            var recipe = await _recipesService.FindById(id);
            var listProduct = await _recipesService.FindAllProductsByRecipe(id);
            foreach (var item in listProduct)
            {
                item.Product = await _productService.FindProductById(item.ProductId);
            }
            var viewModel = new RecipeDetailsViewModel
            {
                Recipes = recipe,
                Products = listProduct
            };

            return View(viewModel);
        }
    }
}