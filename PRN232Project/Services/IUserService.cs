using BusinessObjects;
using DTOs;

namespace Services
{
    public interface IUserService
    {
        Task<UserResponseDto> GetUserByIdAsync(int id);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<User> CreateUserAsync(UserRequestDto createUserDto);
        Task UpdateUserAsync(int id, UserRequestDto updateUserDto);
        Task DeleteUserAsync(int id);
    }
}
