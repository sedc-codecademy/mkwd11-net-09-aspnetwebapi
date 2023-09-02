using Profiles.BLL.Exceptions;
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

        public ProfileService(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
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

        public ProfileModel CreateProfile(CreateProfileModel model)
        {
            var profile = new Profile
            {
                Email = model.Email,
                ImageUrl = model.ImageUrl,
                Phone = model.Phone,
                Username = model.Username,
                YearOfBirth = model.YearOfBirth,
            };
            profileRepository.Add(profile);
            return profile.ToModel();
        }

        public ProfileModel Delete(int id)
        {
            var profile = profileRepository.Get(id) ?? throw new NotFoundException();
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

        public ProfileModel Update(int id, ProfileModel profileModel)
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
