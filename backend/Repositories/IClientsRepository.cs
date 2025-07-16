using backend.DTOs.Dashboard;
using backend.Entities;

namespace backend.Repositories
{
    public interface IClientsRepository
    {
        Task<PaginatorDto<SearchResponseDto>> GetClientsAsync(SearchRequestDto searchRequestDto);
        Task<Client?> GetClientByIdAsync(int id);
        Task<Client> AddClientAsync(Client client);
        Task<bool> UpdateClient(Client client);
        bool ClientExists(int id);
        Task<bool> SaveChangesAsync();
        Task<int> GetNextClientIdAsync();
    }
}