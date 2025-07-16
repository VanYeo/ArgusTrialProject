using backend.DTOs.Login;
using backend.Services.Token;
using Microsoft.AspNetCore.Identity;

namespace backend.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenRepository;

        public LoginService(UserManager<IdentityUser> userManager, ITokenService tokenRepository )
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }
        public async Task<LoginResponseDto?> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var token = _tokenRepository.CreateJWTToken(user);
                return new LoginResponseDto
                {
                    Email = email,
                    JwtToken = token
                };
            }

            return null;
        }
    }
}
