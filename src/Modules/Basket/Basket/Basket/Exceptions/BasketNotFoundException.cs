namespace Basket.Basket.Exceptions;

public class BasketNotFoundException(string username) 
    : NotFoundException("ShoppingCart", username)
{
}
