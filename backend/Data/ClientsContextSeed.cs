using backend.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace backend.Data
{
    public class ClientsContextSeed
    {
        public static async Task SeedAsync(ClientsDbContext context)
        {
            if (!context.Clients.Any())
            {
                try
                {
                    var clientsData = await File.ReadAllTextAsync("Data/clients.json");

                    var clients = JsonSerializer.Deserialize<List<Client>>(clientsData, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (clients == null || clients.Count == 0) return;

                    var passwordHasher = new PasswordHasher<Client>();

                    foreach (var client in clients)
                    {
                        // Hash the GeneratedPassword before saving
                        client.GeneratedPassword = passwordHasher.HashPassword(client, client.GeneratedPassword);
                    }

                    context.Clients.AddRange(clients);
                    await context.SaveChangesAsync();

                    // upd accountnumber to match clientid
                    foreach (var client in context.Clients)
                    {
                        client.AccountNumber = client.ClientID;
                    }

                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[SEED ERROR] Failed to seed clients: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
