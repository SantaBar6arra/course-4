using Common.Models;

namespace Command.Api.Dtos;

public record UpdateClientContactDto(
    Guid Id,
    ContactType Type,
    string Value);
