using MangmentBS.Core.Entities;
using MangmentBS.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Services.Services.Tokens
{
    public class TokenServices : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenServices(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public async Task<string> CreateTokenAsync(Core.Entities.User user)
        {
            var authclaims = new List<Claim>
            {
                new Claim("id", user.Id),
                new Claim("name", user.Name),
                new Claim("role", user.Role),
                new Claim("isActive", user.IsActive.ToString())
            };

            var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var token = new JwtSecurityToken
            (
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: authclaims,
                expires: DateTime.Now.AddDays(double.Parse(configuration["Jwt:DurationInDays"])), 
                signingCredentials: new SigningCredentials(authkey,SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
