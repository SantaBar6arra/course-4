namespace Command.Api.Dtos;

public record CreateProductDto(
    string Name,
    string Description,
    decimal Price,
    uint StockQuantity,
    string Category,
    List<string> Tags);

public record UpdateProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    uint StockQuantity,
    string Category,
    List<string> Tags);
