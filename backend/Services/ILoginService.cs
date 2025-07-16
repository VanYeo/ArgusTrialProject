using backend.DTOs.Login;

namespace backend.Services
{
    public interface ILoginService
    {
        Task<LoginResponseDto?> LoginAsync(string email, string password);
        

    }
}
