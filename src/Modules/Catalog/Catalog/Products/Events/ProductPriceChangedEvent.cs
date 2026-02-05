namespace Catalog.Products.Events;

public sealed record ProductPriceChangedEvent(Product Product) 
    : IDomainEvent;