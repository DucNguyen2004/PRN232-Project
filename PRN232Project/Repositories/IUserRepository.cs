using BusinessObjects;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> AddAsync(User user);
        Task UpdateAsync(int id, User user);
        Task DeleteAsync(int id);
    }
}
