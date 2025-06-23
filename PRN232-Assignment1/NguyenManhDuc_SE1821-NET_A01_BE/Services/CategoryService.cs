using BusinessObjects;
using Repositories;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService()
        {
            _categoryRepository = new CategoryRepository();
        }

        public void AddCategory(Category category)
        {
            _categoryRepository.AddCategory(category);
        }

        public bool DeleteCategory(short id)
        {
            return _categoryRepository.DeleteCategory(id);
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }

        public Category? GetCategoryById(short id)
        {
            return _categoryRepository.GetCategoryById(id);
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.UpdateCategory(category);
        }
    }
}
