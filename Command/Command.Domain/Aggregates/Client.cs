using Common.Events;
using Common.Models;
using Core;

namespace Command.Domain.Aggregates;

public class Client : AggregateRoot
{
    #region Members

    private string _firstName;
    private string _lastName;

    private ClientStatus _status;
    private DateTime _createdAt;
    private DateTime _lastUpdatedAt;

    private Address _address;

    #endregion

    #region Methods

    public Client()
    {

    }

    public Client(
        string firstName,
        string lastName,
        ClientStatus status,
        Address address)
    {
        RaiseEvent(new ClientCreated(
            Guid.NewGuid(),
            firstName,
            lastName,
            status,
            address
        ));
    }

    public void UpdateBaseData(
        string firstName,
        string lastName,
        ClientStatus status)
    {
        RaiseEvent(new ClientBaseDataUpdated(Id, firstName, lastName, status));
    }

    public void UpdateAddress(Address address)
    {
        if (_status is ClientStatus.Deleted)
            throw new InvalidOperationException("entity is already deleted!");

        RaiseEvent(new ClientAddressUpdated(Id, address));
    }

    public void Delete()
    {
        if (_status is ClientStatus.Deleted)
            throw new InvalidOperationException("entity is already deleted!");

        RaiseEvent(new ClientDeleted(Id));
    }

    #endregion

    #region Handle Events

    private void On(ClientCreated @event)
    {
        _id = @event.Id;
        _firstName = @event.FirstName;
        _lastName = @event.LastName;
        _status = @event.Status;
        _createdAt = DateTime.UtcNow;
        _lastUpdatedAt = DateTime.UtcNow;
        _address = @event.Address;
    }

    private void On(ClientBaseDataUpdated @event)
    {
        _firstName = @event.FirstName;
        _lastName = @event.LastName;
        _status = @event.Status;
        _lastUpdatedAt = DateTime.UtcNow;
    }

    private void On(ClientAddressUpdated @event)
    {
        _address = @event.Address;
        _lastUpdatedAt = DateTime.UtcNow;
    }

    private void On(ClientDeleted _)
    {
        _status = ClientStatus.Deleted;
        _lastUpdatedAt = DateTime.UtcNow;
    }

    #endregion
}
