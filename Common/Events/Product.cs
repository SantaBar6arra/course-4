using Core;

namespace Common.Events;

public record ProductCreated(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    uint StockQuantity,
    string Category,
    List<string> Tags) : BaseEvent(Id, typeof(ProductCreated));

public record ProductDetailsUpdated(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Category,
    List<string> Tags) : BaseEvent(Id, typeof(ProductDetailsUpdated));

public record ProductStockQuantityUpdated(
    Guid Id,
    uint StockQuantity) : BaseEvent(Id, typeof(ProductStockQuantityUpdated));

public record ProductDiscontinued(Guid Id) : BaseEvent(Id, typeof(ProductDiscontinued));

public record ProductUnlocked(Guid Id) : BaseEvent(Id, typeof(ProductUnlocked));
