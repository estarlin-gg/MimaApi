using AutoMapper;
using Mima.Application.Dtos;
using Mima.Application.Services.Abstraction;
using Mima.Application.Utils;
using Mima.Domain.Model;
using Mima.Infrastructure.Repositories.Abstraction;

namespace Mima.Application.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly GetUserAuth _getUserAuth;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, GetUserAuth getUserAuth)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _getUserAuth = getUserAuth;
        }

        public async Task CreateCategory(CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                throw new ArgumentNullException(nameof(categoryDto));
            }
            var userId = _getUserAuth.GetUserId();
            var categoryMapped = _mapper.Map<Category>(categoryDto);

            categoryMapped.UserId = userId;

            await _categoryRepository.CreateCategory(categoryMapped);

        }

        public async Task DeleteCategory(int id)
        {
            var existingCategory = await _categoryRepository.GetCategory(id);
            if (existingCategory == null)
            {
                throw new Exception("producto no encontrado");
            }

            await _categoryRepository.DeleteCategory(id);

        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            var userId = _getUserAuth.GetUserId();
            var categories = await _categoryRepository.GetAllCategory(userId);
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task UpdateCategory(int id, CategoryDto categoryDto)
        {
            var existingCategory = await _categoryRepository.GetCategory(id);
            if (existingCategory == null)
            {
                 throw new Exception("producto no encontrado");
            }
            _mapper.Map(categoryDto, existingCategory);

            await _categoryRepository.UpdateCategory(id,existingCategory);

            


        }
    }
}
