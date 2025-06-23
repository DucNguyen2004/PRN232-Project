using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var listCategories = new List<Category>();
            try
            {
                using var context = new MyStoreDbContext();
                listCategories = context.Categories.Include(f => f.Products).ToList();
            }

            catch (Exception ex)
            {
                {
                    throw new Exception(ex.Message);
                }
            }
            return listCategories;
        }
    }
}
