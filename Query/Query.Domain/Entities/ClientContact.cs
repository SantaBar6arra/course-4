using Common.Models;

namespace Query.Domain.Entities;

public class ClientContact
{
    public Guid Id { get; set; }
    public ContactType Type { get; set; }
    public string Value { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    public Guid ClientId { get; set; }
    public Client Client { get; set; }
}
