using Basket.Basket.Features.UpdateItemPriceInBasket;
using MassTransit;
using Shared.Messaging.Events;

namespace Basket.Basket.EventHandlers;

public class ProductPriceChangedIntegrationEventHandler(
    ISender sender,
    ILogger<ProductPriceChangedIntegrationEventHandler> logger)
    : IConsumer<ProductPriceChangedIntegrationEvent>
{
    public async Task Consume(
        ConsumeContext<ProductPriceChangedIntegrationEvent> context)
    {
        logger.LogInformation(
            "Received new Price for ProductId: {ProductId}, New Price: {Price}",    
            context.Message.ProductId, context.Message.Price);

        var command = new UpdateItemPriceInBasketCommand(
            context.Message.ProductId,
            context.Message.Price);

        var result = await sender.Send(command);

        var message = context.Message;
        if (!result.IsSuccess)
        {
            
            logger.LogError(
                "Failed to update item price in basket for ProductId: {ProductId}",
                message.ProductId);
        }

        logger.LogInformation(
            "Successfully updated item price in basket for ProductId: {ProductId}",
            message.ProductId);
    }
}
