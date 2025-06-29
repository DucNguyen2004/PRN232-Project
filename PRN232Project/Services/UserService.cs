using BusinessObjects;
using DTOs;
using Repositories;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<UserResponseDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return Mappers.UserMapper.ToDTO(user);
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(user => Mappers.UserMapper.ToDTO(user));
        }

        public async Task<User> CreateUserAsync(UserRequestDto createUserDto)
        {
            return await _userRepository.AddAsync(Mappers.UserMapper.ToEntity(createUserDto));
        }

        public async Task UpdateUserAsync(int id, UserRequestDto updateUserDto)
        {
            await _userRepository.UpdateAsync(id, Mappers.UserMapper.ToEntity(updateUserDto));
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
