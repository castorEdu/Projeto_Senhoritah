using Microsoft.EntityFrameworkCore;
using Senhoritah.API.Context;
using Senhoritah.API.Migrations;
using Senhoritah.API.Model;

namespace Senhoritah.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly context _context;
        public ProductRepository(context context) 
        { 
            _context = context;
        }
        public async Task<IEnumerable<ProductModel>> FindAll()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<ProductModel> FindProductById(long id)
        {
            var product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            return product;
        }
        public async Task<ProductModel> CreateProduct(ProductModel prod)
        {
            _context.Products.Add(prod);
            await _context.SaveChangesAsync();
            return prod;
        }
        public async Task<ProductModel> UpdateProduct(ProductModel prod)
        {
            _context.Products.Update(prod);
            await _context.SaveChangesAsync();
            return prod;
        }
        public async Task<bool> DeleteProduct(long id)
        {
            try
            {
                ProductModel product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
                if (product == null) return false;
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductModel>> FindByName(string name)
        {
            return await _context.Products.Where(c => c.Name.Contains(name)).ToListAsync();
        }
    }
}
