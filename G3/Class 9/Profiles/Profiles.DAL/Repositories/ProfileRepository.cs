using Profiles.DAL.Data;
using Profiles.DAL.Entities;

namespace Profiles.DAL.Repositories
{
    public class ProfileRepository
        : IProfileRepository
    {
        private readonly ProfileDbContext context;

        public ProfileRepository(ProfileDbContext context)
        {
            this.context = context;
        }

        public void Add(Profile profile)
        {
            context.Profiles.Add(profile);
            context.SaveChanges();
        }

        public void Delete(Profile profile)
        {
            context.Profiles.Remove(profile);
            context.SaveChanges();
        }

        public Profile? Get(int id)
        {
            return context.Profiles.Find(id);
        }

        public IEnumerable<Profile> GetAll()
        {
            return context.Profiles;
        }

        public void Update(Profile profile)
        {
            context.SaveChanges();
        }
    }
}
