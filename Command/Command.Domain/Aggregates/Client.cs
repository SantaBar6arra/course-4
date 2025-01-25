using Common;
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

    private Contact _contact;
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
        Contact contact,
        Address address)
    {
        RaiseEvent(new ClientCreated(
            Guid.NewGuid(),
            firstName,
            lastName,
            status,
            contact,
            address
        ));
    }

    public void UpdateContact(Contact contact)
    {
        if (_status is ClientStatus.Deleted)
            throw new Exception("entity is already deleted!"); // todo add custom exception

        RaiseEvent(new ClientContactUpdated(Id, contact));
    }

    public void UpdateAddress(Address address)
    {
        if (_status is ClientStatus.Deleted)
            throw new Exception("entity is already deleted!");

        RaiseEvent(new ClientAddressUpdated(Id, address));
    }

    public void Delete()
    {
        if (_status is ClientStatus.Deleted)
            throw new Exception("entity is already deleted!");

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
        _createdAt = DateTime.Now;
        _lastUpdatedAt = DateTime.Now;
        _contact = @event.Contact;
        _address = @event.Address;
    }

    private void On(ClientContactUpdated @event) => _contact = @event.Contact;

    private void On(ClientAddressUpdated @event) => _address = @event.Address;

    private void On(ClientDeleted _) => _status = ClientStatus.Deleted;

    #endregion
}
