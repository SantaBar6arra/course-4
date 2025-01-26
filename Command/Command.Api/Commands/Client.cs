using Common.Models;
using Core;

namespace Command.Api.Commands;

public record CreateClient(
    string FirstName,
    string LastName,
    ClientStatus ClientStatus,
    Address Address,
    Contact[] Contacts) : BaseCommand;

public record UpdateClient(
    Guid Id,
    string FirstName,
    string LastName,
    ClientStatus ClientStatus,
    Address Address) : BaseCommand;

public record DeleteClient(Guid Id) : BaseCommand;
