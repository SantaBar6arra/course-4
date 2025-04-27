using Common.Models;

namespace Query.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public decimal TotalAmount { get; set; }

    public List<OrderItem> Items { get; set; } = [];
}
