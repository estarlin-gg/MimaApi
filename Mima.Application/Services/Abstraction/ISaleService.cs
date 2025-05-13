using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mima.Application.Dtos;

namespace Mima.Application.Services.Abstraction
{
    public interface ISaleService
    {
        Task CreateSale(SaleDto saleDto);
        Task<IEnumerable<SaleDto>> GetAllSales();
        Task<SaleDto> GetSaleById(int id);
    }
}
