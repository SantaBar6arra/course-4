using Common.Models;
using Core;

namespace Common;

public record ClientCreated(
    Guid Id,
    string FirstName,
    string LastName,
    ClientStatus Status,
    Contact Contact,
    Address Address) : BaseEvent(Id, nameof(ClientCreated));

public record ClientContactUpdated(Guid Id, Contact Contact)
    : BaseEvent(Id, nameof(ClientContactUpdated));

public record ClientAddressUpdated(Guid Id, Address Address)
    : BaseEvent(Id, nameof(ClientAddressUpdated));

public record ClientDeleted(Guid Id) : BaseEvent(Id, nameof(ClientDeleted));
