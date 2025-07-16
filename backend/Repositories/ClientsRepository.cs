using backend.Data;
using backend.DTOs.Dashboard;
using backend.Entities;
using backend.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace backend.Repositories
{
    public class ClientsRepository(ClientsDbContext context) : IClientsRepository
    {
        public async Task<Client> AddClient(Client client)
        {
            context.Clients.Add(client);
            await context.SaveChangesAsync();
            client.AccountNumber = client.ClientID.ToString("D6");
            await context.SaveChangesAsync();
            return client;
        }

        public bool ClientExists(int id)
        {
            return context.Clients.Any(x => x.ClientID == id);
        }

        public Task<Client?> GetClientByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatorDto<SearchResponseDto>> GetClientsAsync(SearchRequestDto request)
        {
            var query = context.Clients.AsQueryable();

            // Handle Active/Inactive filter
            if (request.SelectedFilters.Contains("active", StringComparer.OrdinalIgnoreCase) &&
                !request.SelectedFilters.Contains("inactive", StringComparer.OrdinalIgnoreCase))
            {
                query = query.Where(c => c.Options.ActiveAccount == true);
            }
            else if (!request.SelectedFilters.Contains("active", StringComparer.OrdinalIgnoreCase) &&
                     request.SelectedFilters.Contains("inactive", StringComparer.OrdinalIgnoreCase))
            {
                query = query.Where(c => c.Options.ActiveAccount == false);
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
                                filters.Add("StartDate.AddMonths(ContractTermMonths ?? 0).Date == @{paramIndex}");
                                values.Add(dateValue.Date);
                            }
                            else
                            {
                                filters.Add($"{actualField} == @{paramIndex}");
                                values.Add(dateValue);
                            }
                        }
                    }
                    else // string
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

            // Sorting
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                var sortField = request.SortBy;
                var sortOrder = string.Equals(request.SortDirection, "desc", StringComparison.OrdinalIgnoreCase)
                    ? "descending"
                    : "ascending";

                query = query.OrderBy($"{sortField} {sortOrder}");
            }
            else
            {
                query = query.OrderBy("ClientID");
            }

            // Projection
            var projected = query.Select(c => new SearchResponseDto
            {
                ClientID = c.ClientID,
                CompanyName = c.CompanyName,
                TradingName = c.TradingName,
                Contact = c.Contact,
                Phone = c.Phone,
                Mobile = c.Mobile,
                Email = c.Email,
                Connections = c.Connections,
                StartDate = c.StartDate,
                ContractTermMonths = c.ContractTermMonths,
                ActiveAccount = c.Options.ActiveAccount
            });

            var pagedList = await PaginatedList<SearchResponseDto>.CreateAsync(projected, request.PageIndex, request.PageSize);

            return new PaginatorDto<SearchResponseDto>
            {
                Items = pagedList,
                PageIndex = pagedList.PageIndex,
                PageSize = pagedList.PageSize,
                TotalPages = pagedList.TotalPages,
                TotalCount = pagedList.TotalCount,
                HasPreviousPage = pagedList.HasPreviousPage,
                HasNextPage = pagedList.HasNextPage
            };
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void UpdateClient(Client client)
        {
            context.Entry(client).State = EntityState.Modified;
        }
    }
}