using Microsoft.EntityFrameworkCore;
using Query.Domain.Entities;

namespace Query.Infrastructure;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientContact> ClientContacts { get; set; }
}
