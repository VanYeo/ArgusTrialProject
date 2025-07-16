using backend.Data;
using backend.DTOs.Dashboard;
using backend.Entities;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController(IClientsRepository repo) : Controller
    {
        [HttpPost]
        public async Task<ActionResult<PaginatorDto<Client>>> SearchClients([FromBody] SearchRequestDto request)
        {
            var results = await repo.GetClientsAsync(request);

            return Ok(results);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateClient(Client client)
        {
            await repo.AddClient(client); // Await the async method
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateClient(int id, Client client)
        {
            if (client.ClientID != id || !ClientExists(id))
            {
                return BadRequest("cannot update client");
            }

            repo.UpdateClient(client);

            if (await repo.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem updating client");
        }


        private bool ClientExists(int id)
        {
            return repo.ClientExists(id);
        }
    }
}

