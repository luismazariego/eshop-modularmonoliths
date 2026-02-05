namespace Catalog.Products.Events;

public sealed record ProductCreatedEvent(Product Product)
    : IDomainEvent;