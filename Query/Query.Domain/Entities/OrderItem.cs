namespace Query.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public uint Quantity { get; set; }

    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}
