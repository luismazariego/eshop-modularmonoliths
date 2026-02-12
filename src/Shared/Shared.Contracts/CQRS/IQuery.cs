using MediatR;

namespace Shared.Contracts.CQRS;

public interface IQuery : IRequest<Unit>;

public interface IQuery<out TResult> : IRequest<TResult> 
    where TResult : notnull;
