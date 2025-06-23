using Microsoft.AspNetCore.Mvc;
using Services;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _context;

        public CategoryController()
        {
            _context = new CategoryService();
        }
        [HttpGet("statistic")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = _context.GetCategories();
            var result = categories.Select(c => new CategoryDTO
            {
                CategoryName = c.CategoryName,
                NumProduct = c.Products.Count()
            }).ToList();

            return result;
        }
    }
}
