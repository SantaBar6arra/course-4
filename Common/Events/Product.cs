using Core;

namespace Common.Events;

public record ProductCreated(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    uint StockQuantity,
    string Category,
    List<string> Tags) : BaseEvent(Id, nameof(ProductCreated));

public record ProductDetailsUpdated(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Category,
    List<string> Tags) : BaseEvent(Id, nameof(ProductDetailsUpdated));

public record ProductStockQuantityUpdated(
    Guid Id,
    uint StockQuantity) : BaseEvent(Id, nameof(ProductStockQuantityUpdated));

public record ProductDiscontinued(Guid Id) : BaseEvent(Id, nameof(ProductDiscontinued));

public record ProductUnlocked(Guid Id) : BaseEvent(Id, nameof(ProductUnlocked));
