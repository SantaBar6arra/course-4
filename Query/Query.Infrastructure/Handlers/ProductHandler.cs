using Common.Events;
using Core;
using Microsoft.EntityFrameworkCore;
using Query.Domain.Entities;

namespace Query.Infrastructure.Handlers;

public class ProductHandler(DataContext context)
    : IEventHandler<ProductCreated>
    , IEventHandler<ProductDetailsUpdated>
    , IEventHandler<ProductStockQuantityUpdated>
    , IEventHandler<ProductDiscontinued>
    , IEventHandler<ProductUnlocked>
{
    private readonly DataContext _context = context;

    public async Task On(ProductCreated @event)
    {
        var product = new Product
        {
            Id = @event.Id,
            Name = @event.Name,
            Description = @event.Description,
            Price = @event.Price,
            StockQuantity = @event.StockQuantity,
            Category = @event.Category,
            IsAvailable = true,
        };

        var tags = @event.Tags.Select(tag => new ProductTag(product.Id, tag));

        await _context.ProductTags.AddRangeAsync(tags);
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task On(ProductDetailsUpdated @event)
    {
        var product = await _context.Products
                .Include(product => product.Tags)
                .SingleOrDefaultAsync(product => product.Id == @event.Id)
            ?? throw new Exception("product not found!");

        product.Name = @event.Name;
        product.Description = @event.Description;
        product.Price = @event.Price;
        product.Category = @event.Category;

        var tagsToDelete = product.Tags.Where(tag => !@event.Tags.Contains(tag.Name));
        var newTags = @event.Tags
            .Where(tag => !product.Tags.Any(productTag => productTag.Name == tag))
            .Select(tag => new ProductTag(product.Id, tag));

        _context.ProductTags.RemoveRange(tagsToDelete);
        await _context.ProductTags.AddRangeAsync(newTags);
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task On(ProductStockQuantityUpdated @event)
    {
        var product = await _context.Products.FindAsync([@event.Id])
            ?? throw new Exception("product not found!");

        product.StockQuantity = @event.StockQuantity;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task On(ProductDiscontinued @event)
    {
        var product = await _context.Products.FindAsync([@event.Id])
            ?? throw new Exception("product not found!");

        product.IsAvailable = false;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task On(ProductUnlocked @event)
    {
        var product = await _context.Products.FindAsync([@event.Id])
            ?? throw new Exception("product not found!");

        product.IsAvailable = true;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}
