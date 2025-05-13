using Mima.Application.Dtos;

namespace Mima.Application.Services.Abstraction
{
    public interface ICategoryService
    {
        Task CreateCategory(CategoryDto categoryDto);
        Task DeleteCategory(int id);
        Task<IEnumerable<CategoryDto>> GetAllCategories();
        Task UpdateCategory(int id, CategoryDto categoryDto);
    }
}
