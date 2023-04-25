using Application.Common.Models.Auth;
using Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class JwtManager
    {
        private readonly JwtSettings _jwtSettings;
        public JwtManager(IOptions<JwtSettings> jwtSettingsOption)
        {
            _jwtSettings = jwtSettingsOption.Value;
        }
        public JwtDto Generate(string userId, string email, string firstName, string lastName, List<string>? roles = null)
        {
            var claims = new List<Claim>()
            {
                new Claim("uid", userId),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.GivenName, firstName),
                new Claim(JwtRegisteredClaimNames.GivenName, lastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            // Algoritma daha sonra AES ile güncellenecek

            var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddMinutes(_jwtSettings.ExpiryInMinutes);

            var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    expires: expiry,
                    claims: claims,
                    signingCredentials: signingCredentials
                    );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);
            return new JwtDto(accessToken, expiry);
        } 
    }
}