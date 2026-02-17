using MassTransit;
using Ordering.Orders.Features.CreateOrder;
using Shared.Messaging.Events;

namespace Ordering.Orders.EventHandlers;

public class BasketCheckoutIntegrationEventHandler
    (ISender sender, ILogger<BasketCheckoutIntegrationEventHandler> logger)
    : IConsumer<BasketCheckoutIntegrationEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutIntegrationEvent> context)
    {
        logger.LogInformation(
            "Integration Event handled: {IntegrationEvent}", 
            context.Message.GetType().Name);

        // Create new order and start order fullfillment process
        var createOrderCommand = MapToCreateOrderCommand(context.Message);
        await sender.Send(createOrderCommand);
    }

    private CreateOrderCommand MapToCreateOrderCommand(
        BasketCheckoutIntegrationEvent message)
    {
        // Create full order with incoming event data
        var addressDto = new AddressDto(
            message.FirstName,
            message.LastName,
            message.EmailAddress,
            message.AddressLine,
            message.Country,
            message.State,
            message.ZipCode);
        var paymentDto = new PaymentDto(
            message.CardName, 
            message.CardNumber, 
            message.Expiration, 
            message.Cvv,
            message.PaymentMethod);
        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.UserName,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            Items:
            [
                new OrderItemDto(
                    orderId, 
                    new Guid("abea18ef-2454-4d2b-9d38-04ebaa2fb3a3"), 2, 1199),
                new OrderItemDto(orderId, 
                    new Guid("f1b2c3d4-5e6f-47a8-9b0c-1d2e3f4a5b67"), 1, 1500)
            ]);

        return new CreateOrderCommand(orderDto);
    }
}