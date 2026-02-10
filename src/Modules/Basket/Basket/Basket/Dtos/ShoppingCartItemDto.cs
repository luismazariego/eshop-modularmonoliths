namespace Basket.Basket.Dtos;

public record ShoppingCartItemDto(
    Guid Id,
    Guid ProductId,
    Guid ShoppingCartId,
    int Quantity,
    string Color,
    string ProductName,
    decimal Price);
