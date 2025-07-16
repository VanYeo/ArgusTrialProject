using backend.Data;
using backend.DTOs.Dashboard;
using backend.Entities;
using backend.Helpers;
using backend.Services.Password;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace backend.Repositories.Clients
{
    public class ClientsRepository(ClientsDbContext context, IPasswordService passwordService) : IClientsRepository
    {

        public async Task<Client> AddClientAsync(Client client)
        {
            client.GeneratedPassword = passwordService.HashPassword(client, client.GeneratedPassword);
            context.Clients.Add(client);
            await context.SaveChangesAsync();
            return client;
        }

        public bool ClientExists(int id)
        {
            return context.Clients.Any(x => x.ClientID == id);
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            if (!ClientExists(id))
            {
                return null;
            }
            var client = await context.Clients.FindAsync(id);

            return client;

        }

        public IQueryable<Client> GetFilteredClientsQueryable(SearchRequestDto request)
        {
            var query = context.Clients.AsQueryable();

            // Active/Inactive Filter
            if (request.SelectedFilters.Contains("active", StringComparer.OrdinalIgnoreCase) &&
                !request.SelectedFilters.Contains("inactive", StringComparer.OrdinalIgnoreCase))
            {
                query = query.Where(c => c.ActiveAccount == true);
            }
            else if (!request.SelectedFilters.Contains("active", StringComparer.OrdinalIgnoreCase) &&
                     request.SelectedFilters.Contains("inactive", StringComparer.OrdinalIgnoreCase))
            {
                query = query.Where(c => c.ActiveAccount == false);
            }

            // Keyword Filtering
            if (!string.IsNullOrWhiteSpace(request.Keyword) && request.SelectedFields?.Count > 0)
            {
                var filters = new List<string>();
                var values = new List<object>();

                foreach (var field in request.SelectedFields)
                {
                    var property = typeof(Client)
                        .GetProperties()
                        .FirstOrDefault(p => p.Name.Equals(field, StringComparison.OrdinalIgnoreCase));

                    if (property == null) continue;

                    string actualField = property.Name;
                    int paramIndex = filters.Count;

                    if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
                    {
                        if (int.TryParse(request.Keyword, out var intValue))
                        {
                            filters.Add($"{actualField} == @{paramIndex}");
                            values.Add(intValue);
                        }
                    }
                    else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                    {
                        if (DateTime.TryParse(request.Keyword, out var dateValue))
                        {
                            if (actualField.Equals("ContractEnd", StringComparison.OrdinalIgnoreCase))
                            {
                                filters.Add("StartDate.AddMonths(ContractTerm ?? 0).Date == @{paramIndex}");
                                values.Add(dateValue.Date);
                            }
                            else
                            {
                                filters.Add($"{actualField} == @{paramIndex}");
                                values.Add(dateValue);
                            }
                        }
                    }
                    else
                    {
                        filters.Add($"{actualField} != null && {actualField}.ToLower().Contains(@{paramIndex})");
                        values.Add(request.Keyword.ToLower());
                    }
                }

                if (filters.Count > 0)
                {
                    var whereClause = string.Join(" || ", filters);
                    query = query.Where(whereClause, values.ToArray());
                }
            }
            query = query.OrderBy(c => c.ClientID);
            return query;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateClient(Client updatedClient)
        {
            var existing = await context.Clients
                .Include(c => c.BillingAddress)
                .Include(c => c.DeliveryAddress)
                .FirstOrDefaultAsync(c => c.ClientID == updatedClient.ClientID);

            if (existing == null) return false;

            context.Entry(existing).CurrentValues.SetValues(updatedClient);

            if (updatedClient.BillingAddress != null)
            {
                context.Entry(existing.BillingAddress!).CurrentValues.SetValues(updatedClient.BillingAddress);
            }

            if (updatedClient.DeliveryAddress != null)
            {
                context.Entry(existing.DeliveryAddress!).CurrentValues.SetValues(updatedClient.DeliveryAddress);
            }

            return await context.SaveChangesAsync() > 0;
        }


        public async Task<int> GetNextClientIdAsync()
        {
            var maxId = await context.Clients.MaxAsync(c => (int?)c.ClientID) ?? 0;
            return maxId + 1;
        }
        public IQueryable<Client> GetClientsQueryable()
        {
            return context.Clients.AsQueryable();
        }


    }
}