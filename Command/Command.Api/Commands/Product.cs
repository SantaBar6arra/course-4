using Core;

namespace Command.Api.Commands;

public record CreateProduct(
    string Name,
    string Description,
    decimal Price,
    uint StockQuantity,
    string Category,
    List<string> Tags) : BaseCommand;

public record UpdateProductDetails(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Category,
    List<string> Tags) : BaseCommand;

public record SellProduct(Guid Id, uint Quantity);

public record UpdateProductStockQuantity(
    Guid Id,
    uint StockQuantity) : BaseCommand;

public record DiscontinueProduct(Guid Id) : BaseCommand;

public record UnlockProduct(Guid Id) : BaseCommand;
