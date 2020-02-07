using MediatR;

namespace BeFaster.Core.Cqrs
{
    public interface ICommandHandler<in TParameter, TResponse>
        : IRequestHandler<TParameter, TResponse>
        where TResponse : IResult where TParameter : ICommand<TResponse>
    {
    }
}
