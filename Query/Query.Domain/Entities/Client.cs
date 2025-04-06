using System;
using Common.Models;

namespace Query.Domain.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public ClientStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }

    public string Country { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int House { get; set; }
    public string PostalCode { get; set; } = string.Empty;

    public IList<ClientContact> Contacts { get; set; } = [];
}
