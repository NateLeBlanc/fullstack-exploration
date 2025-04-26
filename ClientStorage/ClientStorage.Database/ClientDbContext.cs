using ClientStorage.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientStorage.Database
{
    public class ClientDbContext : DbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options)
            : base(options) { }

        public DbSet<Client> Clients { get; set; }
    }
}
