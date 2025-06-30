using BusinessObjects;
using DTOs;
using Repositories;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository;
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

        public async Task<User> CreateUserAsync(UserRequestDto dto)
        {
            var user = Mappers.UserMapper.ToEntity(dto);

            var roles = await _roleRepository.GetAllAsync();
            user.Roles = roles.Where(r => dto.RoleIds.Contains(r.Id)).ToList();

            return await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(int id, UserRequestDto dto)
        {
            User updatedUser = Mappers.UserMapper.ToEntity(dto);

            var roles = await _roleRepository.GetAllAsync();
            updatedUser.Roles = roles.Where(r => dto.RoleIds.Contains(r.Id)).ToList();

            await _userRepository.UpdateAsync(id, updatedUser);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
