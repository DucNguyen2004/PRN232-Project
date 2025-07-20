using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class OrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Order>> GetAllByUserAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .OrderByDescending(o => o.Id)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetByUserWithFilters(int userId, DateTime? start, DateTime? end, string? status)
        {
            var query = _context.Orders
                .Where(o => o.UserId == userId)
                .AsQueryable();

            if (start.HasValue && end.HasValue)
            {
                query = query.Where(o => o.OrderDate >= start && o.OrderDate <= end);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.OrderStatus.ToLower() == status.ToLower());
            }

            return await query
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllWithFilters(DateTime? start, DateTime? end, string? status)
        {
            var query = _context.Orders.AsQueryable();

            if (start.HasValue && end.HasValue)
            {
                query = query.Where(o => o.OrderDate >= start && o.OrderDate <= end);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.OrderStatus.ToLower() == status.ToLower());
            }

            return await query
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .ToListAsync();
        }

        public async Task<Order> SaveAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateStatusByIdsAsync(string status, List<int> ids)
        {
            var orders = await _context.Orders
                .Where(o => ids.Contains(o.Id))
                .ToListAsync();

            foreach (var order in orders)
            {
                order.OrderStatus = status;
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetByStatusAsync(string status)
        {
            return await _context.Orders
                .Where(o => o.OrderStatus.ToLower() == status.ToLower())
                .ToListAsync();
        }

        public async Task<long[]> CountOrdersByStatusAndMonthAsync(string status, int year)
        {
            var result = await _context.Orders
                .Where(o => o.OrderStatus.ToLower() == status.ToLower() && o.OrderDate.Year == year)
                .GroupBy(o => o.OrderDate.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .ToListAsync();

            long[] monthlyCounts = new long[12];
            foreach (var row in result)
            {
                monthlyCounts[row.Month - 1] = row.Count;
            }

            return monthlyCounts;
        }
    }
}
