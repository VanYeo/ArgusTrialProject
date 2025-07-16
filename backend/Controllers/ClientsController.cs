using backend.Data;
using backend.DTOs.Dashboard;
using backend.Entities;
using backend.Repositories.Clients;
using backend.Services.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController(IClientsService service) : Controller
    {
        [HttpPost]
        public async Task<ActionResult<PaginatorDto<Client>>> SearchClients([FromBody] SearchRequestDto request)
        {
            var results = await service.GetFilteredClientsAsync(request);
            return Ok(results);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateClient(Client client)
        {
            var addedClient = await service.AddClientAsync(client);
            return Ok(addedClient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, Client client)
        {
            if (id != client.ClientID)
                return BadRequest("Mismatched ID");

            var success = await service.UpdateClientAsync(client);
            if (success)
                return Ok(client); 

            return NotFound();
        }


        [HttpGet("new-clientID")]
        public async Task<ActionResult<string>> GetNextAccountNumber()
        {
            var nextId = await service.GetNextClientIdAsync();
            return Ok(nextId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            var client = await service.GetClientByIdAsync(id);
            if (client == null) return NoContent();
            return Ok(client);
        }
    }
}

