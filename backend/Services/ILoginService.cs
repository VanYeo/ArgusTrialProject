namespace backend.Services
{
    public interface ILoginService
    {
        Task<(bool IsAuthenticated, string? Token)> LoginAsync(string email, string password);
        

    }
}
