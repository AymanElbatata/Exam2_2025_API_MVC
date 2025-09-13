using Exam2025.DAL.BaseEntity;
using Exam2025.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Exam2025.DAL.Repositories
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;

        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration, UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            _configuration = configuration;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
        }

        public async Task<string> CreateToken(AppUser user)
        {
            string UserRole = "";
            foreach (var item in roleManager.Roles.ToList())
            {
                if (await userManager.IsInRoleAsync(user,item.Name))
                {
                    UserRole = item.Name;
                    break;
                }
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.Role, UserRole),
                new Claim("UserId", user.Id),
                new Claim("DateOfMaking", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                new Claim("DateOfExpiration", DateTime.Now.AddHours(12).ToString("yyyy-MM-dd HH:mm:ss"))
            };

            //var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var creds = new SigningCredentials(_key, SecurityAlgorithms.Aes128CbcHmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = creds,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(12),
                Issuer = _configuration["Token:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
