using APP.Models;

namespace APP.Utils
{
    public class Resorces
    {
        public string nameRecipe {  get; set; } = string.Empty;
        public string descriptionRecipe {  get; set; } = string.Empty;
        public List<ProductModel> productsAdded { get; set; } = new List<ProductModel>();
        public void AddProductToList(ProductModel selectedProduct)
        {
            productsAdded.Add(selectedProduct);
        }
        public void RemoveProductFromList(ProductModel selectedProduct)
        {
            productsAdded.Remove(selectedProduct);
        }
        public void ClearAll()
        {
            nameRecipe = string.Empty;
            descriptionRecipe = string.Empty;
            productsAdded.Clear();
        }
    }
}
