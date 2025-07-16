using Microsoft.AspNetCore.Identity;

namespace backend.Services.Token
{
    public interface ITokenService
    {
        string CreateJWTToken(IdentityUser user);
    }
}
