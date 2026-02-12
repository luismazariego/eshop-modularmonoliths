
using MassTransit;
using Shared.Messaging.Events;

namespace Catalog.Products.Events.EventHandlers;

public class ProductPriceChangedEventHandler(
    IBus bus,
    ILogger<ProductPriceChangedEventHandler> logger)
    : INotificationHandler<ProductPriceChangedEvent>
{
    public async Task Handle(
        ProductPriceChangedEvent notification,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Domain Event handled {DomainEvent}",
            notification.GetType().FullName);

        var integrationEvent = new ProductPriceChangedIntegrationEvent
        {
            ProductId = notification.Product.Id,
            Name = notification.Product.Name,
            Category = notification.Product.Category,
            Description = notification.Product.Description,
            ImageFile = notification.Product.ImageFile,
            Price = notification.Product.Price
        };

        await bus.Publish(integrationEvent, cancellationToken);
    }
}
