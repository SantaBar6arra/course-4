namespace Common.Models;

public enum OrderStatus
{
    Pending,      // Order has been created but not confirmed yet.
    Confirmed,    // Order has been confirmed.
    Shipped,      // Order has been shipped.
    Delivered,    // Order has been delivered.
    Cancelled     // Order has been cancelled.
}
