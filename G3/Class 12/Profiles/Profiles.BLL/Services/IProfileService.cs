using Profiles.BLL.LoggedInUser;
using Profiles.BLL.Models;

namespace Profiles.BLL.Services
{
    public interface IProfileService
    {
        IEnumerable<ProfileModel> GetProfiles();

        ProfileModel CreateProfile(ICurrentUser user, CreateProfileModel model);

        ProfileModel GetProfile(int id);

        ProfileModel Update(ICurrentUser user, int id, ProfileModel profileModel);

        ProfileModel Delete(ICurrentUser user, int id);

        void Connect(int currentProfileId, int otherProfileId);
    }
}