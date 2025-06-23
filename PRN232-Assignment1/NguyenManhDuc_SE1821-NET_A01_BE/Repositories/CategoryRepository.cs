using BusinessObjects;
using DataAccessObjects;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDAO _categoryDAO = new CategoryDAO();

        public List<Category> GetAllCategories()
        {
            return _categoryDAO.GetAllCategories();
        }

        public Category? GetCategoryById(short id)
        {
            return _categoryDAO.GetCategoryById(id);
        }
        public void AddCategory(Category category)
        {
            _categoryDAO.AddCategory(category);
        }

        public void UpdateCategory(Category category)
        {
            _categoryDAO.UpdateCategory(category);
        }

        public bool DeleteCategory(short id)
        {
            return _categoryDAO.DeleteCategory(id);
        }
    }
}
