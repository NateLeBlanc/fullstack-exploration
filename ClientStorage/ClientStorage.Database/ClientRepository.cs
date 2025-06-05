using ClientStorage.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientStorage.Database;

public class ClientRepository(ClientDbContext clientDbContext)
{
    private readonly ClientDbContext _clientDbContext = clientDbContext;

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