using Mima.Domain.Model;

namespace Mima.Infrastructure.Repositories.Abstraction
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategory(string userId);
        Task<Category> GetCategory(int id);
        Task CreateCategory(Category category  );
        Task UpdateCategory(int id, Category category);
        Task DeleteCategory(int id);
    }
}
