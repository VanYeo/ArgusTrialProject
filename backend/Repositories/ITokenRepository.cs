using Microsoft.AspNetCore.Identity;

namespace backend.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user);
    }
}
