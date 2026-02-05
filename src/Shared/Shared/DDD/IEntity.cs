namespace Shared.DDD;

public interface IEntity<T> : IEntity
{
    T Id { get; set; }
}

public interface IEntity
{
    DateTimeOffset? CreatedAt { get; set; }
    string? CreatedBy { get; set; }
    DateTimeOffset? LastModified { get; set; }
    string? LastModifiedBy { get; set; }
}
