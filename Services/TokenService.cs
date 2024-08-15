using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_BurgerManiaBackend.Options;
using API_BurgerManiaBackend.Services;

namespace SecureWebApiSample.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        public TokenService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        public string GenerateToken(string number)
        {
            var tokenNum = new String(number + DateTime.Now);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, tokenNum),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // Refresh tokens can be used to create new jwt tokens
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );
            // this token allows to return a json string to the user by creating the jwt token and returning it as stirng
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
