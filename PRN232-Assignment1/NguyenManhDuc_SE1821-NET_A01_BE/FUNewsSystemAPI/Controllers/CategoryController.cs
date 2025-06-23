using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace FUNewsSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController()
        {
            _categoryService = new CategoryService();
        }

        // GET: api/category
        [HttpGet]
        public ActionResult<List<Category>> GetAllCategories()
        {
            return _categoryService.GetAllCategories();
        }

        // GET: api/category/5
        [HttpGet("{id}")]
        public ActionResult<Category> GetCategoryById(short id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        // POST: api/category
        [HttpPost]
        public IActionResult AddCategory([FromBody] Category category)
        {
            _categoryService.AddCategory(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
        }

        // PUT: api/category
        [HttpPut]
        public IActionResult UpdateCategory([FromBody] Category category)
        {
            var existing = _categoryService.GetCategoryById(category.CategoryId);
            if (existing == null) return NotFound();
            _categoryService.UpdateCategory(category);
            return NoContent();
        }

        // DELETE: api/category/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(short id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null) return NotFound();
            bool deleted = _categoryService.DeleteCategory(id);
            if (!deleted)
            {
                return BadRequest("Failed to delete category. It might be in use.");
            }
            return NoContent();
        }
    }
}
