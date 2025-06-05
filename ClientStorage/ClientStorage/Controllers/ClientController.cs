using ClientStorage.Database;
using ClientStorage.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ClientStorage.Server.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientController(ClientRepository client) : ControllerBase
{
    private readonly ClientRepository _clientRepo = client;

    //TODO: Think about adding a mediator for logic check of valid inputs

    [HttpPost]
    [SwaggerOperation("Add a new clients.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Add clients.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error creating clients.", typeof(ProblemDetails))]
    public async Task<IActionResult> AddClient([FromBody] Client client)
    {
        await _clientRepo.AddClientAsync(client);
        return Ok();
    }

    [HttpGet]
    [SwaggerOperation("Get list of clients.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List clients.", typeof(List<Client>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error getting clients.", typeof(ProblemDetails))]
    public async Task<IActionResult> GetClients()
    {
        List<Client> clients = await _clientRepo.GetClientsAsync();
        return Ok(clients);
    }
}