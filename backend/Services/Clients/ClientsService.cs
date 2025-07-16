using backend.DTOs.Dashboard;
using backend.Entities;
using backend.Helpers;
using backend.Repositories.Clients;
using backend.Services.Password;

namespace backend.Services.Clients
{
    public class ClientsService : IClientsService
    {
        private readonly IClientsRepository _repo;
        private readonly IPasswordService _passwordRepo;

        public ClientsService(IClientsRepository repo, IPasswordService passwordRepo)
        {
            _repo = repo;
            _passwordRepo = passwordRepo;
        }
        public async Task<Client> AddClientAsync(Client client)
        {
            client.GeneratedPassword = _passwordRepo.HashPassword(client, client.GeneratedPassword);
            return await _repo.AddClientAsync(client);
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            return await _repo.GetClientByIdAsync(id);
        }

        public async Task<PaginatorDto<SearchResponseDto>> GetFilteredClientsAsync(SearchRequestDto request)
        {
            var query = _repo.GetFilteredClientsQueryable(request);

            // Projection to DTO
            var projected = query.Select(c => new SearchResponseDto
            {
                ClientID = c.ClientID,
                CompanyName = c.CompanyName,
                TradingName = c.TradingName,
                Contact = c.Contact,
                Phone = c.Phone,
                Mobile = c.Mobile,
                LoginEmail = c.LoginEmail,
                Connections = c.Connections,
                StartDate = c.StartDate,
                ContractTerm = c.ContractTerm,
                ActiveAccount = c.ActiveAccount,
                CustomValue = c.CustomValue
            });

            // Pagination
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

        public async Task<int> GetNextClientIdAsync()
        {
            return await _repo.GetNextClientIdAsync();
        }

        public async Task<bool> UpdateClientAsync(Client client)
        {
            return await _repo.UpdateClient(client);
        }
    }
}
