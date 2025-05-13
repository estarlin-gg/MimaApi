using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mima.Application.Dtos;
using Mima.Application.Services.Abstraction;
using Mima.Application.Utils;
using Mima.Domain.Model;
using Mima.Infrastructure.Repositories.Abstraction;
using Mima.Infrastructure.Repositories.Implementation;

namespace Mima.Application.Services.Implementation
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly GetUserAuth _getUserAuth;

        public SaleService(ISaleRepository saleRepository, IProductRepository productRepository, IMapper mapper, GetUserAuth getUserAuth)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _getUserAuth = getUserAuth;
        }

        public async Task CreateSale(SaleDto saleDto)
        {
            if (saleDto == null || saleDto.SalesProducts.Count == 0)
            {
                throw new ArgumentNullException("La venta debe contener al menos un producto.");
            }

            var userId = _getUserAuth.GetUserId();

            var sale = new Sale
            {
                UserId = userId,
                CustomerName = saleDto.CustomerName,
                SaleDate = DateTime.UtcNow,
                TotalPay = saleDto.SalesProducts.Sum(p => p.Price * p.Quantity),
                SalesProducts = new List<SaleProduct>()
            };

            foreach (var productDto in saleDto.SalesProducts)
            {
                var product = await _productRepository.GetProductById(productDto.ProductId);
                if (product == null)
                {
                    throw new Exception($"Producto con ID {productDto.ProductId} no encontrado.");
                }

                if (product.Stock < productDto.Quantity)
                {
                    throw new Exception($"No hay suficiente stock para el producto '{product.Name}'. Stock actual: {product.Stock}, requerido: {productDto.Quantity}");
                }

                product.Stock -= productDto.Quantity;
                await _productRepository.UpdateProduct(product.Id,product); 

                sale.SalesProducts.Add(new SaleProduct
                {
                    ProductId = productDto.ProductId,
                    ProductName = product.Name,
                    Quantity = productDto.Quantity,
                    Price = product.Price
                });
            }

            await _saleRepository.CreateSale(sale);
        }

        public async Task<IEnumerable<SaleDto>> GetAllSales()
        {
            var userId = _getUserAuth.GetUserId();
            var sales = await _saleRepository.GetAllSales(userId);
            return _mapper.Map<IEnumerable<SaleDto>>(sales);
        }

        public async Task<SaleDto> GetSaleById(int id)
        {
            var userId = _getUserAuth.GetUserId();
            var sale = await _saleRepository.GetSaleById(id);

            if (sale == null)
            {
                throw new Exception("Venta no encontrada.");
            }

            return _mapper.Map<SaleDto>(sale);
        }
    }
}

