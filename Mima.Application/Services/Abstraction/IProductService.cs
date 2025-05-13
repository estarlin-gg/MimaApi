using Mima.Application.Dtos;
using Mima.Domain.Model;

namespace Mima.Application.Services.Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<string> CreateProduct(ProductDto productDto);
        Task<string> UpdateProduct(int id, ProductDto productDto);
        Task<string> DeleteProduct(int id);
    }
}
