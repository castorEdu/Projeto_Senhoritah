using APP.Models;

namespace APP.Utils
{
    public class Resorces
    {
        public string nameRecipe {  get; set; } = string.Empty;
        public string descriptionRecipe {  get; set; } = string.Empty;
        public List<ProductsRecipeModel> productsAdded { get; set; } = new List<ProductsRecipeModel>();
        public int Index { get; set; } = 0;
        public void AddProductToList(ProductsRecipeModel selectedProduct)
        {
            productsAdded.Add(selectedProduct);
        }
        public void RemoveProductFromList(int i)
        {
            var index = i - 1;
            if(i >= 0) productsAdded.RemoveAt(index);
        }
        public void ClearAll()
        {
            nameRecipe = string.Empty;
            descriptionRecipe = string.Empty;
            Index = 0;
            productsAdded.Clear();
        }
        public int SetIndex()
        {
            Index += 1;
            return Index;
        }
    }
}
