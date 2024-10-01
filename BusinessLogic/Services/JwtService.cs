using BusinessLogic.Entities;
using BusinessLogic.Exceptions;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;


namespace BusinessLogic.Services
{
    internal class JwtService(IConfiguration configuration) : IJwtService
    {
        private readonly JwtOptions jwtOpts = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>()
                ?? throw new HttpException("Invalid JWT setting", HttpStatusCode.InternalServerError);

        private IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new ("id", user.Id.ToString()),
                new ("name", user.Name!),
                new ("surname", user.Surname!),
                new ("email", user.Email!),
                new ("birthdate", user.Birthdate.ToShortDateString()),
                new ("username", user.Username ?? ""),
                new ("image", user.Image ?? ""),
                new ("roles", JsonConvert.SerializeObject(user.Roles.Select(x=>x.Name).ToArray())),
            };
            return claims;
        }

        private SigningCredentials getCredentials(JwtOptions options)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }

        public string CreateToken(User user)
        {
            var claims = GetClaims(user);
            var time = DateTime.UtcNow.AddHours(jwtOpts.Lifetime);
            var credentials = getCredentials(jwtOpts);
            var token = new JwtSecurityToken(
                issuer: jwtOpts.Issuer,
                claims: claims,
                expires: time,
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
