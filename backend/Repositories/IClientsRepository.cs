using backend.DTOs.Dashboard;
using backend.Entities;

namespace backend.Repositories
{
    public interface IClientsRepository
    {
        Task<PaginatorDto<SearchResponseDto>> GetClientsAsync(SearchRequestDto searchRequestDto);
        Task<Client?> GetClientByIdAsync(int id);
        Task<Client> AddClient(Client client);
        void UpdateClient(Client client);
        bool ClientExists(int id);
        Task<bool> SaveChangesAsync();
    }
}