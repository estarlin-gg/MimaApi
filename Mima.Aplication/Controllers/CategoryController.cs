using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mima.Application.Dtos;
using Mima.Application.Services.Abstraction;

namespace Mima.Api.Controllers
{
    [Authorize]
    [Route("category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllCategories()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized("No estás autenticado.");
            }
            var categories =await _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null) 
            {
                return BadRequest("producto no valido");
            }

            await _categoryService.CreateCategory(categoryDto);
            return StatusCode(201,"Producto creado con exito");
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Updateproduct(int id,[FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null || id <= 0)
            {
                return BadRequest("id no valido");
            }
            await _categoryService.UpdateCategory(id,categoryDto);
            return StatusCode(204, new { message = "Categoria actualizada con exito" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            if (id <= 0)
            {
                return BadRequest("id no valido");
            }
            await _categoryService.DeleteCategory(id);
            return Ok("categoria eliminada con exito");
        }
    }
}
