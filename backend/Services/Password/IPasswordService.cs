namespace backend.Services.Password
{
    public interface IPasswordService
    {
        string HashPassword<T>(T user, string plainPassword);
    }
}
