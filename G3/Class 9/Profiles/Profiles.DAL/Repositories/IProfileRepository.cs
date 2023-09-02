using Profiles.DAL.Entities;

namespace Profiles.DAL.Repositories
{
    public interface IProfileRepository
    {
        IEnumerable<Profile> GetAll();

        void Add(Profile profile);

        Profile? Get(int id);

        void Update(Profile profile);

        void Delete(Profile profile);
    }
}
