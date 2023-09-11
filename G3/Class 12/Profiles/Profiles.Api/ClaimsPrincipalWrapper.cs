using Profiles.BLL.LoggedInUser;
using System.Security.Claims;
using System.Security.Claims;
namespace Profiles.Api
{
    public class ClaimsPrincipalWrapper :
        ICurrentUser
    {
        private IEnumerable<string> roles = new List<string>();
        public ClaimsPrincipalWrapper(ClaimsPrincipal principal)
        {
            Id = int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));
            Name = principal.FindFirstValue(ClaimTypes.Name);
            roles = principal.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
        }
        public int Id { get; private set; }

        public string Name { get; private set; }

        public bool IsInRole(string role)
        {
            return roles.Contains(role);
        }
    }
}
