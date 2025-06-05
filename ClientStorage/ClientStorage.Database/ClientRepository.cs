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

    public async Task<Client?> DeleteClientByIdAsync(int clientId)
    {
        Client? clientToRemove = _clientDbContext.Clients.FirstOrDefault(client => client.Id == clientId);
        if (clientToRemove is null)
        {
            return null;
        }
        _clientDbContext.Remove(clientToRemove);
        await _clientDbContext.SaveChangesAsync();

        return clientToRemove;
    }
}