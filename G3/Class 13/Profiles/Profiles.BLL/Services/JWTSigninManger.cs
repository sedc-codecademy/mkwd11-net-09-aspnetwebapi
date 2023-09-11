using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Profiles.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.BLL.Services
{
    public class JWTSigninManger : ISignInManager
    {
        private readonly IConfiguration configuration;

        public JWTSigninManger(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string SignIn(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var handler = new JwtSecurityTokenHandler();
            var secret = configuration["SecretKey"];
            var token = handler.CreateToken(new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(5),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)), SecurityAlgorithms.HmacSha512)
            });

            return handler.WriteToken(token);
        }
    }
}
