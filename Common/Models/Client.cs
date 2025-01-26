namespace Common.Models;

public record Address(
    string Country,
    string Region,
    string City,
    string Street,
    int House,
    string PostalCode);

public enum ClientStatus
{
    Active,
    Inactive,
    Deleted,
}
