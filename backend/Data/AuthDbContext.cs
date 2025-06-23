using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace backend.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var hasher = new PasswordHasher<IdentityUser>();

            var user1 = new IdentityUser
            {
                Id = "1",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
            };

            user1.PasswordHash = hasher.HashPassword(user1, "admin123");

            var user2 = new IdentityUser
            {
                Id = "2",
                UserName = "user@gmail.com",
                NormalizedUserName = "USER@GMAIL.COM",
                Email = "user@gmail.com",
                NormalizedEmail = "USER@GMAIL.COM",
            };

            user2.PasswordHash = hasher.HashPassword(user2, "user123");

            builder.Entity<IdentityUser>().HasData(user1, user2);

        }
    }

    public class AuthDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
    {
        public AuthDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ArgusTrialAuthDb;Username=postgres;Password=12345678;");

            return new AuthDbContext(optionsBuilder.Options);
        }
    }
}
