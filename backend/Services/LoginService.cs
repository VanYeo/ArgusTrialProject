using backend.DTOs.Login;
using backend.Repositories;
using Microsoft.AspNetCore.Identity;

namespace backend.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public LoginService(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository )
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        // LoginService.cs
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
