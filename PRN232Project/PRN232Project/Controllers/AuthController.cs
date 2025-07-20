using DTOs;
using Microsoft.AspNetCore.Mvc;
using PRN232Project.Services;

namespace PRN232Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid login request.");
            }

            var result = await _jwtService.Authenticate(loginDto);

            if (result == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                return BadRequest("Refresh token is required.");

            var result = await _jwtService.ValidateRefreshToken(refreshToken);

            if (result == null)
                return Unauthorized("Invalid or expired refresh token.");

            return Ok(result);
        }
    }
}

