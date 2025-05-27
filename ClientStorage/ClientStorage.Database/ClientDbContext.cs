using ClientStorage.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientStorage.Database;

public class ClientDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }

    public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Client>()
            .HasKey(client => client.Id);
    }
}