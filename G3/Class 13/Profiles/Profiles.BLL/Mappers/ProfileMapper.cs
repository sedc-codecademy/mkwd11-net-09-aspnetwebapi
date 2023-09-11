using Profiles.BLL.Models;
using Profiles.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.BLL.Mappers
{
    public static class ProfileMapper
    {
        public static ProfileModel ToModel(this Profile profile)
        {
            return new ProfileModel
            {
                Id = profile.Id,
                Email = profile.Email,
                ImageUrl = profile.ImageUrl,
                Phone = profile.Phone,
                Username = profile.Username,
                YearOfBirth = profile.YearOfBirth
            };
        }
    }
}
