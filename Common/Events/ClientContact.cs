using Common.Models;
using Core;

namespace Common.Events;

public record ClientContactCreated(
    Guid Id,
    Guid ClientId,
    ContactType ContactType,
    string Value) : BaseEvent(Id, typeof(ClientContactCreated));

public record ClientContactUpdated(
    Guid Id,
    ContactType ContactType,
    string Value) : BaseEvent(Id, typeof(ClientContactUpdated));

public record ClientContactDeleted(Guid Id) : BaseEvent(Id, typeof(ClientContactDeleted));
