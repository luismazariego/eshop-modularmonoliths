namespace Catalog.Products.Events.EventHandlers;

public class ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger) 
    : INotificationHandler<ProductCreatedEvent>
{
    public Task Handle(
        ProductCreatedEvent notification,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Domain Event Handled: {DomainEvent}",
            notification.GetType().FullName);
        return Task.CompletedTask;
    }
}
