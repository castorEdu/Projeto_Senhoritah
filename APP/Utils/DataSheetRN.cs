using APP.Models;

namespace APP.Utils
{
    public class DataSheetRN
    {
        public string nameRecipe { get; set; } = string.Empty;
        public string descriptionRecipe { get; set; } = string.Empty;
        public List<ProductsRecipeModel> ListProduct {  get; set; } = new List<ProductsRecipeModel>();
        public List<ProductsRecipeModel> ListProductsDeleted { get; set; } = new List<ProductsRecipeModel>();
        public List<ProductsRecipeModel> ListProductsUpdated { get; set; } = new List<ProductsRecipeModel>();
        public bool isAltered { get; set; } = false;
        public int Index { get; set; } = 0;

        public void __addProductToList(ProductsRecipeModel product)
        {
            ListProduct.Add(product);
            isAltered = true;
        }
        public void __addProductToDeleteList(ProductsRecipeModel product)
        {
            ListProduct.Remove(product);
            if(product.Id != 0)
            {
                ListProductsDeleted.Add(product);
                isAltered = true;
            }
        }
        public ProductsRecipeModel __getProductDeleted(long idProduct, long idRecipe)
        {
            return ListProductsDeleted.Where(p => p.IdProduct == idProduct && p.IdRecipe == idRecipe).FirstOrDefault() ?? new ProductsRecipeModel();
        }
        public void __removeProductFromListDeleted(ProductsRecipeModel prod)
        {
            ListProductsDeleted.Remove(prod);
        }
        public void __removeProductFromList(int i)
        {
            var index = i - 1;
            if (index >= 0) ListProduct.RemoveAt(index);
        }
        public void __updateProductFromList(ProductsRecipeModel product)
        {
            if(product.Id != 0)
            {
                ListProduct.Remove(ListProduct.Where(p => p.Id == product.Id).FirstOrDefault());
                ListProduct.Add(product);
                __addUpdatedProductToList(product);

                isAltered = true;
            }
        }
        public void __addUpdatedProductToList(ProductsRecipeModel product)
        {
            if(ListProductsUpdated.Count != 0)
            {
                ProductsRecipeModel findProduct = ListProductsUpdated.Where(p => p.IdProduct == product.IdProduct).FirstOrDefault() ?? new ProductsRecipeModel();
                if (findProduct.IdProduct > 0)
                {
                    ListProductsUpdated.Remove(findProduct);
                }
            }
            ListProductsUpdated.Add(product);
        }
        public int SetIndex()
        {
            Index += 1;
            return Index;
        }
        public void Clear()
        {
            nameRecipe = string.Empty;
            descriptionRecipe = string.Empty;
            ListProduct.Clear();
            ListProductsDeleted.Clear();
            ListProductsUpdated.Clear();
            Index = 0;
            isAltered = false;
        }
    }
}
