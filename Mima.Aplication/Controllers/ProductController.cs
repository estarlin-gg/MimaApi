using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mima.Application.Dtos;
using Mima.Application.Services.Abstraction;

namespace Mima.Api.Controllers
{
    [Authorize] 
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {

            var res = await _productService.GetAllProducts();
            return Ok(res);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductBuId(int id)
        {

            var res = await _productService.GetProductById(id);
            if (res == null) {
                return NotFound(new { message = "Producto no encontrado" });
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] ProductDto productDto)
        {


            if (productDto == null)
                return BadRequest("Error al crear un producto. Datos inválidos.");

            await _productService.CreateProduct(productDto);

            return StatusCode(201, new { message = "Producto creado con exito" });
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {

            if (productDto == null)
                return BadRequest("Error al actualizar el producto. Datos inválidos.");

            await _productService.UpdateProduct(id,productDto);
            return StatusCode(204, new { message = "Producto actualizado con exito" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return StatusCode(400, new { message = "" });
            }

            await _productService.DeleteProduct(id);
            return Ok($"Producto {id} eliminado con exito");
        }
    }
}
