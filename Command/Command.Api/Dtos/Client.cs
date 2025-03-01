using Common.Models;

namespace Command.Api.Dtos;

public record CreateClientDto(
    string FirstName,
    string LastName,
    ClientStatus Status,
    Address Address,
    Contact[] Contacts);

public record UpdateClientDto(
    Guid Id,
    string FirstName,
    string LastName,
    ClientStatus Status,
    Address Address);
