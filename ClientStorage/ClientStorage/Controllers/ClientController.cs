using ClientStorage.Database;
using ClientStorage.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientStorage.Server.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientController : ControllerBase
{
    private readonly ClientRepository _clientRepo;

    public ClientController(ClientRepository client)
    {
        _clientRepo = client;
    }

    //TODO: Think about adding a mediator for logic check of valid inputs

    [HttpPost]
    public async Task<IActionResult> AddClient([FromBody] Client client)
    {
        await _clientRepo.AddClientAsync(client);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetClients()
    {
        var clients = await _clientRepo.GetClientsAsync();
        return Ok(clients);
    }
}