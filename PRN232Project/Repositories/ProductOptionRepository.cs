using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class ProductOptionRepository : IProductOptionRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductOptionRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<ProductOption>> GetAllAsync()
        {
            return await _context.ProductOptions.ToListAsync();
        }

        public async Task<List<ProductOption>> GetByIdsAsync(List<int> ids)
        {
            return await _context.ProductOptions
                .Where(po => ids.Contains(po.Id))
                .ToListAsync();
        }
    }
}
