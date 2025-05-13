using Microsoft.AspNetCore.Mvc;
using Mima.Application.Dtos;
using Mima.Application.Services.Abstraction;

namespace Mima.Api.Controllers
{
    [Route("sale")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetAllSales()
        {
           var res = await _saleService.GetAllSales();

            return Ok(res);
        }

        
        [HttpPost]
        public async Task<ActionResult> CreateSale ([FromBody] SaleDto sale)
        {
            await _saleService.CreateSale(sale);    
            return Ok($"venta creada con exito");
        }


    }
}
