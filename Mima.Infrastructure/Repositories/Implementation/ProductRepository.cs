using Microsoft.EntityFrameworkCore;
using Mima.Domain.Model;
using Mima.Infrastructure.Repositories.Abstraction;

namespace Mima.Infrastructure.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly MimaContext _context;

        public ProductRepository(MimaContext context)
        {
            _context = context;
        }

        public async Task CreateProduct(Product product)
        {
             await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception("Producto no encontrado");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }


        public async Task<Product> GetProductById(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            //if (product == null) throw new Exception("producto no encontrado");
            if (product == null) return null;

            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("El ID de usuario no puede ser nulo o vacío.");
            }

            return await _context.Products.Where(p => p.UserId == userId).Include(p=> p.Category).ToListAsync();
        }


        public async Task UpdateProduct(int productId, Product product)
        {
            var existingProduct = await _context.Products.FindAsync(productId);
            if (existingProduct == null)
            {
                throw new Exception("Producto no encontrado");
            }
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Price = product.Price;
            existingProduct.Discount = product.Discount;
            existingProduct.UserId = product.UserId;

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
        }

    }
}
