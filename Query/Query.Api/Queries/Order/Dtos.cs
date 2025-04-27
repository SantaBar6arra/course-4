using Common.Models;

namespace Query.Api.Queries.Order;

public record OrderDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemDto> Items { get; set; }
}

public record OrderItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public uint Quantity { get; set; }
}

