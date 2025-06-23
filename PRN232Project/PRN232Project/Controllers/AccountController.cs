using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232Project.Dtos;
using PRN232Project.Services;

namespace PRN232Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AccountController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto request)
        {
            var result = await _jwtService.Authenticate(request);
            if (result == null)
                return Unauthorized();

            return result;
        }

        [AllowAnonymous]
        [HttpPost("Refresh")]
        public async Task<ActionResult<LoginResponseDto?>> Refresh([FromBody] RefreshRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Token))
                return BadRequest("Invalid Token");

            var result = await _jwtService.ValidateRefreshToken(request.Token);
            return result is not null ? result : Unauthorized();
        }
    }
}
