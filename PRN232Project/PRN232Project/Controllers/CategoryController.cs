using BusinessObjects;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using PRN232Project.Utils;
using Services;

namespace PRN232Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ApiControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponseDto<CategoryResponseDto>>> GetCategoryById(int id)
        {
            var category = await GetEntityOrThrowAsync(
                () => _categoryService.GetCategoryByIdAsync(id),
                id,
                nameof(Category));
            return OkResponse(category);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<CategoryResponseDto>>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return OkResponse(categories);
        }
    }
}
