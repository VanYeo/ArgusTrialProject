using backend.DTOs.Dashboard;
using backend.Entities;

namespace backend.Repositories.Clients
{
    public interface IClientsRepository
    {
        Task<Client?> GetClientByIdAsync(int id);
        Task<Client> AddClientAsync(Client client);
        Task<bool> UpdateClient(Client client);
        bool ClientExists(int id);
        Task<bool> SaveChangesAsync();
        Task<int> GetNextClientIdAsync();
        IQueryable<Client> GetFilteredClientsQueryable(SearchRequestDto request);

    }
}