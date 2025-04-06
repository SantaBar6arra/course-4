using System;
using Common.Models;

namespace Query.Api.Queries.Client;

public record ListClientDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public ClientStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}

public record ClientDto : ListClientDto
{
    public string Country { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int House { get; set; }
    public string PostalCode { get; set; } = string.Empty;

    public IList<ClientContactDto> Contacts { get; set; } = [];
}

public record ClientContactDto
{
    public Guid Id { get; set; }
    public ContactType Type { get; set; }
    public string Value { get; set; } = string.Empty;
}
