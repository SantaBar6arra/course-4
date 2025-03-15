using Command.Api.Commands;
using Command.Domain.Aggregates;
using Core;

namespace Command.Api.Handlers;

public interface IProductCommandHandler
{
    Task HandleAsync(CreateProduct command);
    Task HandleAsync(UpdateProductDetails command);
    Task HandleAsync(UpdateProductStockQuantity command);
    Task HandleAsync(DiscontinueProduct command);
    Task HandleAsync(UnlockProduct command);
}

public class ProductCommandHandler(IEventSourcingHandler<Product> productHandler)
    : IProductCommandHandler
{
    public async Task HandleAsync(CreateProduct command)
    {
        var (name, description, price, stockQuantity, category, tags) = command;
        var product = new Product(name, description, price, stockQuantity, category, tags);

        await productHandler.SaveAsync(product);
    }

    public async Task HandleAsync(UpdateProductDetails command)
    {
        var (productId, name, description, price, category, tags) = command;
        var product = await productHandler.GetByIdAsync(productId);

        product.UpdateDetails(name, description, price, category, tags);

        await productHandler.SaveAsync(product);
    }

    public async Task HandleAsync(UpdateProductStockQuantity command)
    {
        var (productId, stockQuantity) = command;
        var product = await productHandler.GetByIdAsync(productId);

        product.UpdateStock(stockQuantity);

        await productHandler.SaveAsync(product);
    }

    public async Task HandleAsync(DiscontinueProduct command)
    {
        var product = await productHandler.GetByIdAsync(command.Id);
        product.Discontinue();
        await productHandler.SaveAsync(product);
    }

    public async Task HandleAsync(UnlockProduct command)
    {
        var product = await productHandler.GetByIdAsync(command.Id);
        product.Unlock();
        await productHandler.SaveAsync(product);
    }
}
