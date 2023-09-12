using DataAccess;
using DataModels;
using InterfaceModels;
using Microsoft.Extensions.Options;
using System.Text;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Services.Exceptions;
using System.Text.RegularExpressions;
using Configurations;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserDto> _userRepository;
        private readonly AppSettings _appSettings;
        public UserService(IRepository<UserDto> userRepository,
            IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _appSettings = options.Value;
        }

        public UserModel Authenticate(string username, string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            var user = _userRepository.GetAll().SingleOrDefault(x =>
                x.Username == username && x.Password == hashedPassword);

            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userModel = new UserModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Token = tokenHandler.WriteToken(token)
            };
            return userModel;
        }

        public void Register(RegisterModel model)
        {
            // Validations
            if (string.IsNullOrEmpty(model.FirstName))
                throw new UserException(null, model.Username,
                    "First name is required");
            if (string.IsNullOrEmpty(model.LastName))
                throw new UserException(null, model.Username,
                    "Last name is required");
            if (!ValidUsername(model.Username))
                throw new UserException(null, model.Username,
                    "Username is already in use");
            if (!ValidPassword(model.Password))
                throw new UserException(null, model.Username,
                    "Password is too weak");
            if (model.Password != model.ConfirmPassword)
                throw new UserException(null, model.Username,
                    "Passwords did not match!");

            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(model.Password));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            var user = new UserDto()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Password = hashedPassword
            };

            _userRepository.Add(user);
        }
        private static bool ValidPassword(string password)
        {
            var passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            var match = passwordRegex.Match(password);
            return match.Success;
        }

        private bool ValidUsername(string username)
        {
            return _userRepository.GetAll().All(x => x.Username != username);
        }
    }
}
