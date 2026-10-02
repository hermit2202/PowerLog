using System.Text;
using Microsoft.Extensions.Configuration;
using PowerLog.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace PowerLog.Core.Services
{
    public class AuthService
    {
        private readonly IConfiguration configuration;

        public AuthService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var secretkey = configuration["JwtSettings:SecretKey"]
                ?? throw new InvalidOperationException("JwtSettings:SecretKey not configured");
            var key = Encoding.UTF8.GetBytes(secretkey);
            var symmetricSecurityKey = new SymmetricSecurityKey(key);

            var creds = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Coach"),
            };

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(configuration["JwtSettings:ExpirationInMinutes"] ?? "60")),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
