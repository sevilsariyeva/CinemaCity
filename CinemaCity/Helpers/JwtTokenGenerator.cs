﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CinemaCity.Helpers
{
    public class JwtTokenGenerator
    {
        public static string GenerateToken(string userId, string email, string role, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = configuration["Jwt:SecretKey"];
            var expiryMinutes = configuration.GetValue<int>("Jwt:ExpiryMinutes");
            var audience = configuration["Jwt:Audience"];
            var issuer = configuration["Jwt:Issuer"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("JWT SecretKey is not configured.");
            }

            var key = Encoding.UTF8.GetBytes(secretKey);
            var expiryTime = DateTime.UtcNow.AddMinutes(expiryMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim("aud", audience)
                }),
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token= tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
