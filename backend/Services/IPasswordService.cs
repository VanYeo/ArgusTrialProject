namespace backend.Services
{
    public interface IPasswordService
    {
        string HashPassword<T>(T user, string plainPassword);
    }
}
