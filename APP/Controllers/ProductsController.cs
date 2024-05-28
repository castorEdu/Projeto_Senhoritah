using APP.Models;
using APP.Services.IService;
using APP.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;

namespace APP.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductServices _productService;
        private readonly IUnitService _unitService;
        public ProductsController(IProductServices productServices, IUnitService unitService)
        {
            _productService = productServices;
            _unitService = unitService;

        }
        public async Task<ActionResult> ProductIndex(ProductModel prod)
        {
            var unit = await _unitService.FindAll();
            IEnumerable<string> listUnit = from u in unit select u.Abbreviation;
            var product = await _productService.FindAll();
            var viewModel = new ProductViewModel
            {
                //ProductModel = prod,
                Products = product,
                Units = new SelectList(listUnit)
            };
            return View(viewModel);
        }
        public async Task<ActionResult> SearchProduct(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var products = await _productService.FindProductByName(Name);
                if (products == null) products = Enumerable.Empty<ProductModel>();
                return Ok(products);
            }
            else
            {
                var products = await _productService.FindAll();
                if (products == null) products = Enumerable.Empty<ProductModel>();
                return Ok(products);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            var un = await _unitService.FindByName(product.Unit);
            product.ProductModel.IdUnit = un.Id; 
            product.ProductModel.Unit = un;

            var response = await _productService.CreateProduct(product.ProductModel);
            if (response == null) return RedirectToAction(nameof(ProductIndex));
            response.IsSave = true;
            return RedirectToAction(nameof(ProductIndex));
        }
        [HttpPost]
        public async Task<ActionResult> DeleteProduct(long id)
        {
            var statusProductRecipes = await _productService.DeleteProductRecipes(id);
            var status = await _productService.DeleteProduct(id);
            return RedirectToAction(nameof(ProductIndex));
        }
        public async Task<ActionResult> EditProduct(ProductViewModel product)
        {
            var unit = await _unitService.FindByName(product.Unit);
            product.ProductModel.Unit = unit;
            product.ProductModel.IdUnit = unit.Id;
            var response = await _productService.UpdateProduct(product.ProductModel);
            return RedirectToAction(nameof(ProductIndex));
        }
        public async Task<ActionResult> ProductDetails(long id)
        {
            var SelectedProduct = await _productService.FindProductById(id);
            var unit = await _unitService.FindById(SelectedProduct.IdUnit);
            SelectedProduct.Unit = unit;
            var units = await _unitService.FindAll();
            IEnumerable<string> listUnit = from u in units select u.Abbreviation;
            if (SelectedProduct == null) return BadRequest();
            var viewModel = new ProductViewModel
            {
                ProductModel = SelectedProduct,
                Unit = SelectedProduct.Unit.Abbreviation,
                Units = new SelectList(listUnit)
            };
            return View(viewModel);
        }
        public async Task<int> IsProductInAnyRecipe(long id)
        {
            var products = await _productService.FindProductInRecipes(id);
            return products.ToList().Count();
        }
    }
}
