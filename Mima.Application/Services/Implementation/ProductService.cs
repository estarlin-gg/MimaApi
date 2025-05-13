using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mima.Application.Cache;
using Mima.Application.Dtos;
using Mima.Application.Services.Abstraction;
using Mima.Application.Utils;
using Mima.Domain.Model;
using Mima.Infrastructure.Repositories.Abstraction;

namespace Mima.Application.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly GetUserAuth _getUserAuth;
        private readonly ICacheService _cache;

        public ProductService(IProductRepository repository, IMapper mapper, GetUserAuth getUserAuth, ICacheService cache)
        {
            _repository = repository;
            _mapper = mapper;
            _getUserAuth = getUserAuth;
            _cache = cache;
        }

        public async Task<string> CreateProduct(ProductDto productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto));
            }

            var userId = _getUserAuth.GetUserId();

            var product = _mapper.Map<Product>(productDto);
            product.UserId = userId;
            product.Stock = productDto.Stock > 0 ? productDto.Stock : 0;

            await _repository.CreateProduct(product);

            return $"Producto creado exitosamente. Precio final con descuento: {product.FinalPrice:C}";
        }

        public async Task<string> UpdateProduct(int id, ProductDto productDto)
        {
            var userId = _getUserAuth.GetUserId();
            var existingProduct = await _repository.GetProductById(id);

            if (existingProduct == null || existingProduct.UserId != userId)
                throw new Exception("Producto no encontrado o no autorizado.");

            _mapper.Map(productDto, existingProduct);

            if (productDto.Discount.HasValue)
            {
                existingProduct.Discount = productDto.Discount.Value;
            }

            await _repository.UpdateProduct(id, existingProduct);

            return $"Producto actualizado exitosamente. Precio final con descuento: {existingProduct.FinalPrice:C}";
        }

        public async Task<string> DeleteProduct(int id)
        {
            var userId = _getUserAuth.GetUserId();
            var existingProduct = await _repository.GetProductById(id);

            if (existingProduct == null || existingProduct.UserId != userId)
                throw new BadHttpRequestException("Producto no encontrado o no autorizado.");

            await _repository.DeleteProduct(id);

            return "Producto eliminado exitosamente.";
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            string cachekey = "all_product";
            var cached = _cache.Get<IEnumerable<Product>>(cachekey);
            if (cached != null) return cached;

            var userId = _getUserAuth.GetUserId();

            var products = await _repository.GetProducts(userId);
            _cache.Set(cachekey, products);
            return products;
        }

        public Task<Product> GetProductById(int id)
        {
             var product = _repository.GetProductById(id) ?? null;

             return product;

        }
    }
}
