using Profiles.BLL.Exceptions;
using Profiles.BLL.Models;
using Profiles.DAL.Entities;
using Profiles.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IHasher hasher;
        private readonly IUserRepository userRepository;
        private readonly IEmailSender emailSender;
        private readonly ISignInManager signInManager;

        public UserService(IHasher hasher, IUserRepository userRepository, IEmailSender emailSender, ISignInManager signInManager)
        {
            this.hasher = hasher;
            this.userRepository = userRepository;
            this.emailSender = emailSender;
            this.signInManager = signInManager;
        }
        public void Register(UserModel model)
        {
            ValidateUser(model);

            var code = Guid.NewGuid().ToString();
            var user = new User
            {
                Email = model.Email,
                Password = hasher.HashPassword(model.Password),
                ConfirmationCode = code
            };
            userRepository.Create(user);
            emailSender.SendConfirmationEmail(user.Email, code);
        }


        public string Login(UserLoginModel model)
        {
            var user = userRepository.GetByEmail(model.Email);
            if (user == null)
            {
                return string.Empty;
            }

            if (!hasher.ValidatePassword(model.Password, user.Password))
            {
                return string.Empty;
            }

            return signInManager.SignIn(user);
        }

        private void ValidateUser(UserModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                throw new ValidationException("Password doesn't match confirm password");
            }
            // TODO add more validation
        }
    }
}
