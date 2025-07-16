using Microsoft.AspNetCore.Identity;

namespace backend.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<object> _hasher = new();

        public string HashPassword<T>(T user, string plainPassword)
        {
            return _hasher.HashPassword(user!, plainPassword);
        }

    }
}
