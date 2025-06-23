using BusinessObjects;
using DataAccessObjects;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetCategories()
        {
            return CategoryDAO.GetCategories();
        }
    }
}
