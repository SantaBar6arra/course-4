namespace Common.Models;

public enum ContactType
{
    Email,
    Phone
}

public record Contact(
    ContactType Type,
    string Value);
