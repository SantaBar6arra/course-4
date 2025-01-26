using Common.Events;
using Common.Models;
using Core;

namespace Command.Domain.Aggregates;

public class ClientContact : AggregateRoot
{
    #region Members

    private Guid _clientId;
    private ContactType _type;
    private string _value;

    private bool _isDeleted;

    #endregion

    public ClientContact()
    {
    }

    public ClientContact(
        Guid clientId,
        ContactType type,
        string value)
    {
        RaiseEvent(new ClientContactCreated(
            Guid.NewGuid(),
            clientId,
            type,
            value));
    }

    public void Update(ContactType contactType, string value)
    {
        if (_isDeleted)
            throw new Exception("client contact is already deleted");

        RaiseEvent(new ClientContactUpdated(Id, contactType, value));
    }

    public void Delete()
    {
        if (_isDeleted)
            throw new Exception("client contact is already deleted");

        RaiseEvent(new ClientContactDeleted(Id));
    }

    #region On

    private void On(ClientContactCreated @event)
    {
        _clientId = @event.ClientId;
        _type = @event.ContactType;
        _value = @event.Value;
        _isDeleted = false;
    }

    private void On(ClientContactUpdated @event)
    {
        _type = @event.ContactType;
        _value = @event.Value;
        _isDeleted = false;
    }

    private void On(ClientContactDeleted @event)
    {
        _isDeleted = true;
    }

    #endregion
}
