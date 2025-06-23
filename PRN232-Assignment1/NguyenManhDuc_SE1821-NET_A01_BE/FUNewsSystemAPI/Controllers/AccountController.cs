using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace FUNewsSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string adminEmail = config["DefaultAdminAccount:Email"];
            string adminPassword = config["DefaultAdminAccount:Password"];

            _accountService = new AccountService(adminEmail, adminPassword);
        }

        [HttpGet("users")]
        public ActionResult<List<SystemAccount>> GetAllUsers()
        {
            return _accountService.GetAllUsers();
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] SystemAccount newUser)
        {
            try
            {
                _accountService.CreateUser(newUser);
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.AccountId }, newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<SystemAccount> GetUserById(short id)
        {
            var user = _accountService.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("validate")]
        public ActionResult<SystemAccount> ValidateUser([FromBody] LoginRequest request)
        {
            var user = _accountService.ValidateUser(request.Email, request.Password);
            if (user == null) return Unauthorized("Invalid credentials");
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(short id)
        {
            try
            {
                _accountService.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/toggle-status")]
        public IActionResult ToggleStatus(short id)
        {
            _accountService.ToggleAccountStatus(id);
            return NoContent();
        }

        [HttpPut("{id}/role")]
        public IActionResult UpdateUserRole(short id, [FromBody] int role)
        {
            _accountService.UpdateUserRole(id, role);
            return NoContent();
        }

        [HttpPut("update")]
        public IActionResult UpdateAccount([FromBody] SystemAccount account)
        {
            _accountService.UpdateAccount(account);
            return NoContent();
        }

        [HttpPost("change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordRequest request)
        {
            bool success = _accountService.ChangePassword(request.UserId, request.CurrentPassword, request.NewPassword);
            if (!success) return BadRequest("Password change failed.");
            return Ok("Password updated successfully.");
        }
    }
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class ChangePasswordRequest
    {
        public short UserId { get; set; }
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}