using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _context;

        public ProductController(IProductService context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return _context.GetProducts();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = _context.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            try
            {
                _context.UpdateProduct(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.GetProductById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.SaveProduct(product);
            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = _context.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.DeleteProduct(product);

            return NoContent();
        }

        [HttpGet("filter-price")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> FilterPrice(int? start, int? end)
        {
            if (start == null || end == null)
            {
                return BadRequest("Start and End prices must not be null.");
            }
            if (start > end)
            {
                return BadRequest("Start must be greater or equal end");
            }

            var products = _context.FilterPrice(start.Value, end.Value);
            var res = products.Select(p => new ProductDTO
            {
                ProductName = p.ProductName,
                UnitsInStock = p.UnitsInStock.Value,
                UnitPrice = p.UnitPrice.Value,
                CategoryName = p.Category.CategoryName
            }).ToList();

            if (res.Count == 0)
            {
                return NotFound();
            }
            return res;
        }
    }
}
