using Profiles.BLL.Models;

namespace Profiles.BLL.Services
{
    public interface IProfileService
    {
        IEnumerable<ProfileModel> GetProfiles();

        ProfileModel CreateProfile(CreateProfileModel model);

        ProfileModel GetProfile(int id);

        ProfileModel Update(int id, ProfileModel profileModel);

        ProfileModel Delete(int id);

        void Connect(int currentProfileId, int otherProfileId);
    }
}