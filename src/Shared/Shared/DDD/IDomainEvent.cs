using MediatR;

namespace Shared.DDD;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    DateTimeOffset OccurredOn => DateTimeOffset.UtcNow;
    public string EventType => GetType().AssemblyQualifiedName!;
}
