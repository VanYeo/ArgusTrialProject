using backend.Entities;
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

                    context.Clients.AddRange(clients);
                    await context.SaveChangesAsync();
                    
                    // upd accountnumber to match clientid
                    foreach (var client in context.Clients)
                    {
                        client.AccountNumber = client.ClientID.ToString("D6");
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
