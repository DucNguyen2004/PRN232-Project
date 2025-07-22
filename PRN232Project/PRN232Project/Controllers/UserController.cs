using BusinessObjects;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using PRN232Project.Utils;
using Services;

namespace PRN232Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<UserResponseDto>>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            if (users == null || !users.Any())
            {
                throw ProblemException.NotFound("No user found.");
            }

            return OkResponse(users);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<ApiResponseDto<PaginationResponseDto<UserResponseDto>>>> GetPagedUsers([FromQuery] PaginationRequestDto request)
        {
            var result = await _userService.GetUsersPagedAsync(request);
            if (result == null || !result.Items.Any())
            {
                throw ProblemException.NotFound("No user found.");
            }
            return OkResponse(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponseDto<UserResponseDto>>> GetUserById(int id)
        {
            var user = await GetEntityOrThrowAsync(
                () => _userService.GetUserByIdAsync(id),
                id,
                nameof(User));

            return OkResponse(user);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<UserResponseDto>>> CreateUser([FromBody] UserRequestDto dto)
        {
            if (dto == null)
            {
                throw ProblemException.BadRequest("Invalid user data.");
            }

            User user = await _userService.CreateUserAsync(dto);
            return CreatedResponse(nameof(GetUserById), new { id = user.Id }, Mappers.UserMapper.ToDTO(user));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserRequestDto dto)
        {
            if (dto == null)
                throw ProblemException.BadRequest("Invalid user data.");

            await GetEntityOrThrowAsync(
                () => _userService.GetUserByIdAsync(id),
                id,
                nameof(User));

            await _userService.UpdateUserAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await GetEntityOrThrowAsync(
                () => _userService.GetUserByIdAsync(id),
                id,
                nameof(User));

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
