using backend.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace backend.Data
{
    public class ClientsDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Client> Clients { get; set; }
        public ClientsDbContext(DbContextOptions<ClientsDbContext> options) : base(options)
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
    }
