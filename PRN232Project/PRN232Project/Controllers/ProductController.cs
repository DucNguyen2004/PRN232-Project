using BusinessObjects;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using PRN232Project.Utils;
using Services;

namespace PRN232Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ApiControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<ProductResponseDto>>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();

            if (products == null || !products.Any())
            {
                throw ProblemException.NotFound("No product found.");
            }

            return OkResponse(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponseDto<ProductResponseDto>>> GetProductById(int id)
        {
            var product = await GetEntityOrThrowAsync(
                () => _productService.GetProductByIdAsync(id),
                id,
                nameof(Product));

            return OkResponse(product);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<ProductResponseDto>>> CreateProduct([FromBody] ProductRequestDto dto)
        {
            if (dto == null)
            {
                throw ProblemException.BadRequest("Invalid product data.");
            }

            Product product = await _productService.CreateProductAsync(dto);
            return CreatedResponse(nameof(GetProductById), new { id = product.Id }, Mappers.ProductMapper.ToDTO(product));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] ProductRequestDto dto)
        {
            if (dto == null)
                throw ProblemException.BadRequest("Invalid user data.");

            await GetEntityOrThrowAsync(
                () => _productService.GetProductByIdAsync(id),
                id,
                nameof(User));

            await _productService.UpdateProductAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await GetEntityOrThrowAsync(
                () => _productService.GetProductByIdAsync(id),
                id,
                nameof(Product));

            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
