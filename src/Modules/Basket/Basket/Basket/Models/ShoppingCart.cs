namespace Basket.Basket.Models;

public class ShoppingCart : Aggregate<Guid>
{
    public string Username { get; private set; } = default!;
    private readonly List<ShoppingCartItem> _items = [];
    public IReadOnlyList<ShoppingCartItem> Items => _items.AsReadOnly();
    public decimal TotalPrice => _items.Sum(i => i.Price * i.Quantity);

    public static ShoppingCart Create(Guid id, string username)
    {
        ArgumentException.ThrowIfNullOrEmpty(username, nameof(username));

        var shoppingCart = new ShoppingCart
        {
            Id = id,
            Username = username
        };
        return shoppingCart;
    }

    public void AddItem(
        Guid productId,
        int quantity,
        string color,
        decimal price,
        string productName)
    {
        var existingItem = _items
            .FirstOrDefault(i => i.ProductId == productId && 
                            i.Color == color);
        
        if (existingItem is not null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            var newItem = new ShoppingCartItem(
                Id,
                productId,
                quantity,
                color,
                price,
                productName);

            _items.Add(newItem);
        }
    }

    public void RemoveItem(Guid productId)
    {
        var existingItem = _items
            .FirstOrDefault(i => i.ProductId == productId);
        
        if (existingItem is not null)
        {
            _items.Remove(existingItem);
        }
    }
}
