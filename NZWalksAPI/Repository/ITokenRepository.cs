using Microsoft.AspNetCore.Identity;

namespace NZWalksAPI.Repository
{
    public interface ITokenRepository
    {
        String CreateJwtToken(IdentityUser user, List<String> roles);
        void CreateJwtToken();
    }
}
