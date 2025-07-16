using backend.DTOs.Login;

namespace backend.Services.Login
{
    public interface ILoginService
    {
        Task<LoginResponseDto?> LoginAsync(string email, string password);
    }
}
