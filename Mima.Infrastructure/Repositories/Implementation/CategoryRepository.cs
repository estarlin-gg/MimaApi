using Microsoft.EntityFrameworkCore;
using Mima.Domain.Model;
using Mima.Infrastructure.Repositories.Abstraction;

namespace Mima.Infrastructure.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MimaContext _context;

        public CategoryRepository(MimaContext context)
        {
            _context = context;
        }

        public async Task CreateCategory(Category category)
        {
            var isExist = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category.Name);
            if (isExist != null)
            {
                throw new Exception("La categoría ya existe");
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new Exception("categoria no encontrada");
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategory(string userId)
        {
            return await _context.Categories.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            var res = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (res == null)
            {
                throw new Exception();
            };

            return res;
        }

        public async Task UpdateCategory(int id, Category category)
        {
            var _category = _context.Categories.FirstOrDefault(c => c.Id == id) ?? throw new Exception("producto no encontrado");
            _category.Name = category.Name;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
