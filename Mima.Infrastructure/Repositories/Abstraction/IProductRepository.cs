
using Mima.Domain.Model;

namespace Mima.Infrastructure.Repositories.Abstraction
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts(string userId);
        Task<Product> GetProductById (int productId);
        Task CreateProduct (Product product);
        Task UpdateProduct (int productId,Product product);
        Task DeleteProduct (int productId);

    }
}

