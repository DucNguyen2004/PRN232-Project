﻿using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .Include(u => u.Addresses)
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Roles)
                .Include(u => u.Addresses)
                .Include(u => u.Orders)
                .ToListAsync();
        }

        public async Task<User> AddAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task UpdateAsync(int id, User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var tracked = await _context.Users.FindAsync(id);
            if (tracked == null)
                throw new KeyNotFoundException($"User with Id {id} not found.");

            user.Id = tracked.Id;
            _context.Entry(tracked).CurrentValues.SetValues(user);

            tracked.Roles.Clear();
            foreach (var role in user.Roles)
            {
                tracked.Roles.Add(role);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with Id {id} not found.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
