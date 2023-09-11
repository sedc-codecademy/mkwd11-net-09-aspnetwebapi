using Profiles.BLL.Exceptions;
using Profiles.BLL.LoggedInUser;
using Profiles.BLL.Mappers;
using Profiles.BLL.Models;
using Profiles.DAL.Data;
using Profiles.DAL.Entities;
using Profiles.DAL.Repositories;

namespace Profiles.BLL.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository profileRepository;
        private readonly IUserRepository userRepository;

        public ProfileService(IProfileRepository profileRepository, IUserRepository userRepository)
        {
            this.profileRepository = profileRepository;
            this.userRepository = userRepository;
        }

        public void Connect(int currentProfileId, int otherProfileId)
        {
            var me = profileRepository.Get(currentProfileId) ?? throw new NotFoundException();
            var other = profileRepository.Get(otherProfileId) ?? throw new NotFoundException();

            me.ConnectionFroms.Add(new Connection
            {
                From = me,
                To = other
            });
            profileRepository.Update(me);
        }

        public ProfileModel CreateProfile(ICurrentUser currentUser, CreateProfileModel model)
        {
            var user = userRepository.GetUser(currentUser.Id);
            var profile = new Profile
            {
                Email = model.Email,
                ImageUrl = model.ImageUrl,
                Phone = model.Phone,
                Username = model.Username,
                YearOfBirth = model.YearOfBirth,
                User = user,
            };
            profileRepository.Add(profile);
            return profile.ToModel();
        }

        public ProfileModel Delete(ICurrentUser user, int id)
        {
            var profile = profileRepository.Get(id) ?? throw new NotFoundException();
            if(profile.User.Id != user.Id)
            {
                throw new Exception();
            }
            
            profileRepository.Delete(profile);
            return profile.ToModel();
        }

        public ProfileModel GetProfile(int id)
        {
            var profile = profileRepository.Get(id);

            if (profile == null)
            {
                throw new NotFoundException();
            }
            return profile.ToModel();
        }

        public IEnumerable<ProfileModel> GetProfiles()
        {
            var profiles = profileRepository.GetAll();
            return profiles.Select(x => x.ToModel());
        }

        public ProfileModel Update(ICurrentUser user, int id, ProfileModel profileModel)
        {
            if(id != profileModel.Id)
            {
                throw new Exception();
            }
            var profile = profileRepository.Get(profileModel.Id);
            if(profile == null)
            {
                throw new NotFoundException();
            }

            if (profile.User.Id != user.Id) // || user.IsInRole("Admin"))
            {
                throw new Exception();
            }
            profile.Email = profileModel.Email;
            profile.ImageUrl = profileModel.ImageUrl;
            profile.Phone = profileModel.Phone;
            profile.Username = profileModel.Username;
            profile.YearOfBirth = profileModel.YearOfBirth;
            profileRepository.Update(profile);

            return profile.ToModel();
        }
    }
}
