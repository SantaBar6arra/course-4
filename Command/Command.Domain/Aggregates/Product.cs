using System;
using Common.Events;
using Core;

namespace Command.Domain.Aggregates;

public class Product : AggregateRoot
{
    #region Fields

    private Guid _productId;
    private string _name;
    private string _description;
    private decimal _price;
    private uint _stockQuantity;
    private string _category;
    private bool _isAvailable;
    private List<string> _tags;

    #endregion

    #region Handle Command

    public Product() { }

    public Product(string name, string description, decimal price,
        uint stockQuantity, string category, List<string> tags)
    {
        RaiseEvent(new ProductCreated(
            Guid.NewGuid(), name, description, price, stockQuantity, category, tags));
    }

    public void UpdateDetails(
        string name, string description, decimal price, string category, List<string> tags)
    {
        if (!_isAvailable)
            throw new InvalidOperationException("product is already unavailable!");

        RaiseEvent(new ProductDetailsUpdated(Id, name, description, price, category, tags));
    }

    public void UpdateStock(uint quantity)
    {
        if (!_isAvailable)
            throw new InvalidOperationException("product is already unavailable!");

        RaiseEvent(new ProductStockQuantityUpdated(Id, quantity));
    }

    public void Sell(uint quantity)
    {
        if (!_isAvailable)
            throw new InvalidOperationException("product is already unavailable!");

        if (quantity > _stockQuantity) // cant sell more than we have
            throw new InvalidOperationException("Not enough quantity available!");

        var newStockQuantity = _stockQuantity - quantity;
        RaiseEvent(new ProductStockQuantityUpdated(Id, newStockQuantity));
    }

    public void Discontinue()
    {
        if (!_isAvailable)
            throw new InvalidOperationException("product is already unavailable!");

        RaiseEvent(new ProductDiscontinued(Id));
    }

    public void Unlock()
    {
        if (_isAvailable)
            throw new InvalidOperationException("product is already available!");

        RaiseEvent(new ProductUnlocked(Id));
    }

    #endregion

    #region On 

    private void On(ProductCreated @event)
    {
        _id = @event.Id;
        _name = @event.Name;
        _description = @event.Description;
        _price = @event.Price;
        _stockQuantity = @event.StockQuantity;
        _category = @event.Category;
        _tags = @event.Tags;

        _isAvailable = @event.StockQuantity > 0;
    }

    private void On(ProductDetailsUpdated @event)
    {
        _name = @event.Name;
        _description = @event.Description;
        _price = @event.Price;
        _category = @event.Category;
        _tags = @event.Tags;
    }

    private void On(ProductStockQuantityUpdated @event)
    {
        _stockQuantity = @event.StockQuantity;
        _isAvailable = @event.StockQuantity > 0;
    }

    private void On(ProductDiscontinued _)
    {
        _isAvailable = false;
    }

    private void On(ProductUnlocked _)
    {
        _isAvailable = true;
    }

    #endregion
}
