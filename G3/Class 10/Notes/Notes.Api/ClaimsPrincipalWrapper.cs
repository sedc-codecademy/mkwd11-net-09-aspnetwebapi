using Notes.Services.User;
using System.Security.Claims;

namespace Notes.Api
{
    public class ClaimsPrincipalWrapper
        : ICurrentUser
    {
        public ClaimsPrincipalWrapper(ClaimsPrincipal principal)
        {
            Id = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier).Value);
            Name = principal.FindFirst(ClaimTypes.Name).Value;
        }
        public int Id { get; private set; }

        public string Name { get; private set; }
    }
}
