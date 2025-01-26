using Common.Models;
using Core;

namespace Common.Events;

public record ClientContactCreated(
    Guid Id,
    Guid ClientId,
    ContactType ContactType,
    string Value) : BaseEvent(Id, nameof(ClientContactCreated));

public record ClientContactUpdated(
    Guid Id,
    ContactType ContactType,
    string Value) : BaseEvent(Id, nameof(ClientContactUpdated));

public record ClientContactDeleted(Guid Id) : BaseEvent(Id, nameof(ClientContactDeleted));
