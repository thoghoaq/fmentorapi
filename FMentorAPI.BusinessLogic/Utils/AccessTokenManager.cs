using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace FMentorAPI.BusinessLogic.Utils
{
    public class AccessTokenManager
    {
        public static string GenerateJwtToken(string phoneNumber, string[]? roles, string? userId,
            IConfiguration configuration)
        {
            var tokenConfig = configuration.GetSection("Token");
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.MobilePhone, phoneNumber),
                new Claim(ClaimTypes.NameIdentifier, userId ?? string.Empty)
            };

            if (roles != null && roles.Length > 0)
            {
                permClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(permClaims),
                Expires = DateTime.Now.AddMinutes(Convert.ToDouble(configuration["Token:AccessTokenExpiryTime"])),
                SigningCredentials = credentials
            };
            var token = jwtSecurityTokenHandler.CreateToken(tokenDescription);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}