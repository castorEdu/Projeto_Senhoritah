using APP.Models;
using APP.Models.ViewModel;
using APP.Services.IService;
using APP.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace APP.Controllers
{
    public class DataSheetController : Controller
    {
        private readonly IProductServices _productService;
        private readonly IRecipesService _recipesService;
        private readonly IUnitService _unitService;
        private DataSheetRN _RN;
        public DataSheetController(IProductServices productServices, IRecipesService recipesService, IUnitService unitService, DataSheetRN RN)
        {
            _productService = productServices;
            _recipesService = recipesService;
            _unitService = unitService;
            _unitService = unitService;
            _RN = RN;

        }
        public async Task<IActionResult> DataSheetIndex()
        {
            _RN = new DataSheetRN();
            var response = await _recipesService.FindAll();
            return View(response);
        }
        public async Task<IActionResult> DataSheetProductSession() 
        {
            var unit = await _unitService.FindAll();
            IEnumerable<string> listUnit = from u in unit select u.Abbreviation;
            var view = new DataSheetViewModel
            {
                Products = await _productService.FindAll(),
                AddedProducts = _RN.ListProduct,
                Units = new SelectList(listUnit),
                Recipe = new RecipesModel { Name = _RN.nameRecipe}
            };
            return View(view);
        }
        public async Task<ActionResult> Search(string name)
        {
            var searchResult = Enumerable.Empty<ProductModel>();
            if (!string.IsNullOrEmpty(name)) searchResult = await _productService.FindProductByName(name);

            if (searchResult == null) searchResult = Enumerable.Empty<ProductModel>();

            return Ok(searchResult);
        }
        public async Task<ActionResult> SelectedProduct(long id)
        {
            var product = await _productService.FindProductById(id);
            if (product == null) product = new ProductModel();
            return Ok(product);
        }
        public async Task<ActionResult> AddProduct(long Id, DataSheetViewModel model)
        {
            if(Id > 0)
            {
                if (model.ProductsRecipe.Unit == null || model.ProductsRecipe.Amount == 0 || model.ProductsRecipe.Weight == 0) return RedirectToAction(nameof(DataSheetProductSession));
                var units = await _unitService.FindByName(model.ProductsRecipe.Unit);
                    var product = await _productService.FindProductById(Id);
                    var prod = new ProductsRecipeModel
                    {
                        Product = product,
                        IdProduct = product.Id,
                        ProductName = product.Name,
                        Unit = model.ProductsRecipe.Unit,
                        Weight = model.ProductsRecipe.Weight,
                        Amount = model.ProductsRecipe.Amount,
                        Index = _RN.SetIndex(),
                        IdUnit = units.Id
                    };
                    _RN.__addProductToList(prod);
            }
            return RedirectToAction(nameof(DataSheetProductSession));
        }
        public ActionResult RemoveAddedProducts(int Index)
        {
            _RN.__removeProductFromList(Index);
            return RedirectToAction(nameof(DataSheetProductSession));
        }
        public ActionResult PreSave()
        {
            if (!string.IsNullOrEmpty(_RN.nameRecipe))
            {
                if (_RN.ListProduct.Count > 0)
                {
                    var details = new PreSaveDetailsViewModel
                    {
                        Name = _RN.nameRecipe,
                        products = _RN.ListProduct
                    };
                    return View(details);
                }
            }
            return RedirectToAction(nameof(DataSheetProductSession));
        }
        public async Task<ActionResult> Save() 
        {
            var newRecipe = new RecipesModel();
            try
            {
                newRecipe = await _recipesService.Create(new RecipesModel
                {
                    Name = _RN.nameRecipe,
                    Description = "",
                    Price = 0
                });
            }
            catch(Exception ex)
            {
                throw new Exception($"Falha ao criar Ficha Tecnica. ERROR MESSAGE: {ex.Message}");
            }

            try
            {
                foreach (var item in _RN.ListProduct)
                {
                    var products = new ProductsRecipeModel
                    {
                        IdProduct = item.IdProduct,
                        ProductName = item.ProductName,
                        Product = item.Product,
                        IdRecipe = newRecipe.Id,
                        Amount = item.Amount,
                        Unit = item.Unit,
                        Weight = item.Weight,
                        Price = 0,
                        IdUnit = item.IdUnit,
                    };

                    if (products != null)
                    {
                       var newProductRecipe =  await _recipesService.CreateRecipeProduct(products);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Falha ao inserir registro. ERROR MESSAGE: {ex.Message}");
            }

            return RedirectToAction(nameof(DataSheetIndex));
        }
        public async Task<ActionResult> Edit(RecipeDetailsViewModel model)
        {
            if(string.IsNullOrEmpty(_RN.nameRecipe) || string.IsNullOrEmpty(_RN.descriptionRecipe)) return RedirectToAction(nameof(DataSheetIndex));
            var recipeModel = new RecipesModel
            {
                Id = model.Recipes.Id,
                Name = _RN.nameRecipe,
                Description = _RN.descriptionRecipe,
                Price = 0
            };
            var newRecipeModel = await _recipesService.Update(recipeModel);

            foreach(var item in _RN.ListProduct)
            {
                if(item.Id == 0)
                {
                    await _recipesService.CreateRecipeProduct(item);
                }
            }

            foreach (var item in _RN.ListProductsUpdated)
            {
                await _recipesService.UpdateRecipeProduct(item);
            }

            foreach (var item in _RN.ListProductsDeleted)
            {
                await _recipesService.DeleteRecipeProduct(item.IdRecipe, item.IdProduct);
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
            var unit = await _unitService.FindAll();
            IEnumerable<string> listUnit = from u in unit select u.Abbreviation;

            var recipe = await _recipesService.FindById(id);
            if(!_RN.isAltered)
            {
                var products = await _recipesService.FindAllProductsByRecipe(id);
                _RN.ListProduct = products.ToList();
            }
            else
            {
                recipe = new RecipesModel
                {
                    Id = id,
                    Name = _RN.nameRecipe,
                    Description = _RN.descriptionRecipe
                };
            }
            
            foreach (var item in _RN.ListProduct)
            {
                item.Product = await _productService.FindProductById(item.IdProduct);
                item.Unit = unit.Where(u => u.Id == item.IdUnit).FirstOrDefault().Abbreviation;
            }
            var viewModel = new RecipeDetailsViewModel
            {
                Recipes = recipe,
                Products = _RN.ListProduct,
                ProductsDeleted = _RN.ListProductsDeleted,
                Units = new SelectList(listUnit)
            };

            return View(viewModel);
        }
        public async Task<ActionResult> RecipeDetailsProduct(long idRecipe, long idProduct)
        {
            var product = _RN.ListProduct.Where(p => p.IdRecipe == idRecipe && p.IdProduct == idProduct).FirstOrDefault();
            return View(product);
        }
        public ActionResult RecipeProductUpdate(ProductsRecipeModel model)
        {
            long id = model.IdRecipe;
            _RN.__updateProductFromList(model);

            return RedirectToAction(nameof(RecipeDetails), new { id });
        }        
        public async Task<ActionResult> RecipeDetailsProductDelete(long idRecipe, long idProduct)
        {
            ProductsRecipeModel product = _RN.ListProduct.Where(p => p.IdProduct == idProduct).FirstOrDefault() ?? new ProductsRecipeModel();
            _RN.__addProductToDeleteList(product);

            long id = idRecipe;
            return RedirectToAction(nameof(RecipeDetails), new { id });
        }
        public async Task<ActionResult> AddedProductEdit(long idProduct, RecipeDetailsViewModel model)
        {
            long id = model.Recipes.Id;
            if (string.IsNullOrEmpty(model.ProductsRecipe.Unit) || model.ProductsRecipe.Amount == 0 || model.ProductsRecipe.Weight == 0) return RedirectToAction(nameof(RecipeDetails), new { id });
            var unit = await _unitService.FindByName(model.ProductsRecipe.Unit);
            ProductsRecipeModel product = new ProductsRecipeModel();
            product.Product = await _productService.FindProductById(idProduct);
            product.IdProduct = idProduct;
            product.Unit = model.ProductsRecipe.Unit;
            product.Amount = model.ProductsRecipe.Amount;
            product.Weight = model.ProductsRecipe.Weight;
            product.IdRecipe = id;
            product.IdUnit = unit.Id;

            _RN.__addProductToList(product);

            return RedirectToAction(nameof(RecipeDetails), new { id });
        }
        public ActionResult Undo(long idProduct, long idRecipe)
        {
            ProductsRecipeModel product = _RN.__getProductDeleted(idProduct, idRecipe);
            _RN.__addProductToList(product);
            _RN.__removeProductFromListDeleted(product);
            long id = idRecipe;
            return RedirectToAction(nameof(RecipeDetails), new { id });
        }
        [HttpGet]
        public async Task<ActionResult> Details(long id)
        {
            var recipe = new RecipeDetailsViewModel
            { 
                Recipes = await _recipesService.FindById(id),
                Products = await _recipesService.FindAllProductsByRecipe(id)
            };
            foreach(var product in recipe.Products)
            {
                product.Product = await _productService.FindProductById(product.IdProduct);
                var unit = await _unitService.FindById(product.IdUnit);
                product.Unit = unit.Abbreviation;
            }

            return View(recipe);
        }
        
        public void OnNameChange(string name)
        {
            _RN.nameRecipe = name;
            _RN.isAltered = true;
        }
        public void OnDescriptionChange(string desc)
        {
            _RN.descriptionRecipe = desc;
            _RN.isAltered = true;
        }
    }
}