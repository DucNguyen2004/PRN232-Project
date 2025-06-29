using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessObjects;
using DTOs;
using Microsoft.IdentityModel.Tokens;

namespace PRN232Project.Services
{
    public class JwtService
    {
        private IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<LoginResponseDto?> Authenticate(LoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return null;

            var userAccount = new User(); // change later using dbcontext
            if (userAccount is null) // can have password hash
            {
                return null;
            }

            return await GenerateJwtToken(userAccount);


        }

        //public async Task<LoginResponseDto?> ValidateRefreshToken(string token)
        //{
        //    var refreshToken = await _refreshTokenRepository.Get(token);
        //    if (refreshToken is null || refreshToken.Expiry < DateTime.UtcNow)
        //    {
        //        return null;
        //    }

        //    await _refreshTokenRepository.Delete(refreshToken);

        //    var user = await _userRepository.Get(refreshToken.UserId);
        //    if (user is null) return null;

        //    return await GenerateJwtToken(user);
        //}

        private async Task<LoginResponseDto?> GenerateJwtToken(User user)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("Jwt:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, user.Email)
                }),
                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha512Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new LoginResponseDto
            {
                Email = user.Email,
                AccessToken = accessToken,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds,
            };
        }

        private async Task<string> GenerateRefreshToken(int userId)
        {
            var refreshTokenValidityMins = _configuration.GetValue<int>("Jwt:RefreshTokenValidityMins");
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expiry = DateTime.UtcNow.AddMinutes(refreshTokenValidityMins),
                UserId = userId,
            };

            return refreshToken.Token;
        }
    }
}
