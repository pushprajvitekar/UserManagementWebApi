using Microsoft.AspNetCore.Identity;

namespace UserManagement.Application.Contracts
{
    public interface ITokenProvider
    {
        public string CreateToken(IdentityUser user, IEnumerable<string> roles);
    }
}
