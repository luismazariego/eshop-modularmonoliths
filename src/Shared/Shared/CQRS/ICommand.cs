using MediatR;

namespace Shared.CQRS;

public interface ICommand : ICommand<Unit>;

public interface ICommand<out TResult> : IRequest<TResult>;
