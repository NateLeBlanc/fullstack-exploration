using ClientStorage.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientStorage.Database;

public class ClientRepository
{
    private readonly ClientDbContext _clientDbContext;

    public ClientRepository(ClientDbContext clientDbContext)
    {
        _clientDbContext = clientDbContext;
    }

    public async Task AddClientAsync(Client client)
    {
        _clientDbContext.Clients.Add(client);
        await _clientDbContext.SaveChangesAsync();
    }

    public async Task<List<Client>> GetClientsAsync()
    {
        return await _clientDbContext.Clients.ToListAsync();
    }
}