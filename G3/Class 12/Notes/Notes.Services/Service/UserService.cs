using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Notes.Data.Domain;
using Notes.Data.Repositories;
using Notes.Services.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Notes.Services.Service
{
    public class UserService
        : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher hasher;
        private readonly IConfiguration configuration;

        public UserService(IUserRepository userRepository, IPasswordHasher hasher, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.hasher = hasher;
            this.configuration = configuration;
        }

        public string Login(string email, string password)
        {
            var user = userRepository.GetByEmail(email);
            if (user == null)
            {
                return string.Empty;
            }

            if (!hasher.Verify(password, user.Password))
            {
                return string.Empty;
            }
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                };
            return GenerateToken(claims);
        }

        private string GenerateToken(List<Claim> claims)
        {
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

        public RegisterResult Register(string email, string password, string name)
        {
            RegisterResult result = Validate(email, password);
            if (result.IsSuccess)
            {
                var user = new Notes.Data.Domain.User
                {
                    Email = email,
                    Name = name,
                    Password = hasher.Hash(password)
                };
                userRepository.Create(user);
            }
            return result;
        }

        private RegisterResult Validate(string email, string password)
        {
            if(string.IsNullOrEmpty(email))
            {
                return new RegisterResult
                {
                    Message = "Email is required",
                };
            }

            if (string.IsNullOrEmpty(password))
            {
                return new RegisterResult
                {
                    Message = "Password is required",
                };
            }
            var validEmailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            
            if (!validEmailRegex.IsMatch(email))
            {
                return new RegisterResult
                {
                    Message = "Invalid email",
                };
            }
            var passwordRegex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            if (!passwordRegex.IsMatch(password))
            {
                return new RegisterResult
                {
                    Message = "Password is too weak"
                };
            }
            return new RegisterResult();
        }
    }
}
