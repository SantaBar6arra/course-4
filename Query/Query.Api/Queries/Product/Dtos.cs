namespace Query.Api.Queries.Product;

public record ListProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public uint StockQuantity { get; set; }
    public string Category { get; set; } = string.Empty;
}

public record ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public uint StockQuantity { get; set; }
    public string Category { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public IEnumerable<string> Tags { get; set; } = [];
}
