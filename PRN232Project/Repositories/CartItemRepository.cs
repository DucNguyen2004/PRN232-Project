using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext _context;

        public CartItemRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<CartItem>> GetAllCartItems(int userId) // change parameter ?
        {
            return await _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                    .ThenInclude(p => p.ProductImages)
                .Include(c => c.ProductOption)
                    .ThenInclude(po => po.OptionValue)
                        .ThenInclude(ov => ov.Option)
                .ToListAsync();
        }

        public async Task<CartItem?> GetByIdAsync(int id)
        {
            return await _context.CartItems
                .Include(c => c.Product)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CartItem> AddToCartAsync(CartItem item)
        {
            _context.CartItems.Add(item);
            await _context.SaveChangesAsync();

            return await _context.CartItems
                                .Include(ci => ci.User)
                                .Include(ci => ci.Product)
                                    .ThenInclude(p => p.ProductImages)
                                .Include(ci => ci.ProductOption)
                                    .ThenInclude(po => po.OptionValue)
                                        .ThenInclude(ov => ov.Option)
                                .FirstAsync(ci => ci.Id == item.Id);
        }

        public async Task UpdateAsync(CartItem item)
        {
            _context.CartItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteByUserIdAsync(int userId)
        {
            var items = await _context.CartItems.Where(c => c.UserId == userId).ToListAsync();
            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int userId, int productId)
        {
            return await _context.CartItems.AnyAsync(c => c.UserId == userId && c.ProductId == productId);
        }
    }
}
