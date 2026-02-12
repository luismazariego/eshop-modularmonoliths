namespace Shared.Messaging.Events;

public record IntegrationEvent
{
    public Guid EventId => Guid.NewGuid();
    public DateTimeOffset OccurredOn => DateTimeOffset.UtcNow;
    public string EventType => GetType().AssemblyQualifiedName ?? string.Empty;
}
