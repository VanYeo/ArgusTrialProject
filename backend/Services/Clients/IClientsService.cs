using backend.DTOs.Dashboard;
using backend.Entities;

namespace backend.Services.Clients
{
    public interface IClientsService
    {
        Task<Client> AddClientAsync(Client client);
        Task<bool> UpdateClientAsync(Client client);
        Task<Client?> GetClientByIdAsync(int id);
        Task<int> GetNextClientIdAsync();
        Task<PaginatorDto<SearchResponseDto>> GetFilteredClientsAsync(SearchRequestDto request);
    }
}
