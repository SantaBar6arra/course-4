using Common.Models;
using Core;

namespace Command.Api.Commands;

public record UpdateClientContact(
    Guid Id,
    ContactType Type,
    string Value) : BaseCommand;

public record DeleteClientContact(Guid Id) : BaseCommand;
