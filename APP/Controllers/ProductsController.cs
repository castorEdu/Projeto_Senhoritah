using APP.Models;
using APP.Services.IService;
using APP.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APP.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductServices _productService;
        public ProductsController(IProductServices productServices)
        {
            _productService = productServices;
        }
        public async Task<ActionResult> ProductIndex(ProductModel prod)
        {
            var product = await _productService.FindAll();
            var viewModel = new ProductViewModel
            {
                //ProductModel = prod,
                Products = product
            };
            return View(viewModel);
        }
        public async Task<ActionResult> SearchProduct(string Name)
        {
            if (Name != null)
            {
                var products = await _productService.FindProductByName(Name);
                if (products == null) products = Enumerable.Empty<ProductModel>();
                var viewModel = new ProductViewModel
                {
                    ProductModel = new ProductModel(),
                    Products = products
                };
                return View(viewModel);
            }
            return RedirectToAction(nameof(ProductIndex));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(product.ProductModel);
                if (response == null) return RedirectToAction(nameof(ProductIndex));
                response.IsSave = true;
                return RedirectToAction(nameof(ProductIndex));
            }
            return RedirectToAction(nameof(ProductIndex));
        }
        [HttpPost]
        public async Task<ActionResult> DeleteProduct(long id)
        {
            var status = await _productService.DeleteProduct(id);
            return RedirectToAction(nameof(ProductIndex));
        }
        public async Task<ActionResult> EditProduct(ProductModel product)
        {
            var response = await _productService.UpdateProduct(product);
            return RedirectToAction(nameof(ProductIndex));
        }
        public async Task<ActionResult> ProductDetails(long id)
        {
            var SelectedProduct = await _productService.FindProductById(id);
            if (SelectedProduct == null) return BadRequest();
            return View(SelectedProduct);
        }
        public async Task<ActionResult> AddProduct(long id)
        {
            TempData.Clear();
            var product = await _productService.FindProductById(id);
            if (product == null) return NotFound();
            string response = JsonConvert.SerializeObject(product);
            TempData[product.Id.ToString()] = response.ToString();
            //_contextAccessor.HttpContext?.Session.SetString(id.ToString(), id.ToString());
            return RedirectToAction("");
        }
        public ActionResult GetAddedProducts()
        {
            var list = TempData.ToList();
            foreach (var item in list)
            {
                var value = item.Value.ToString();
                var prod = JsonConvert.DeserializeObject<ProductModel>(value);
            }
            return RedirectToAction("");
        }
    }
}
