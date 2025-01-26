using Common.Models;
using Core;

namespace Common.Events;

public record ClientCreated(
    Guid Id,
    string FirstName,
    string LastName,
    ClientStatus Status,
    Address Address) : BaseEvent(Id, nameof(ClientCreated));

public record ClientBaseDataUpdated(
    Guid Id,
    string FirstName,
    string LastName,
    ClientStatus Status) : BaseEvent(Id, nameof(ClientBaseDataUpdated));

public record ClientAddressUpdated(Guid Id, Address Address)
    : BaseEvent(Id, nameof(ClientAddressUpdated));

public record ClientDeleted(Guid Id) : BaseEvent(Id, nameof(ClientDeleted));
