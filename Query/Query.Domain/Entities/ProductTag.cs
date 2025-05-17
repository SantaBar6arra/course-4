namespace Query.Domain.Entities;

public class ProductTag
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; }

    public ProductTag(Guid productId, string name)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Name = name;
    }
}
