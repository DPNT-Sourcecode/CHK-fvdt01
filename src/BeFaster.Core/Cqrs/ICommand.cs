using MediatR;

namespace BeFaster.Core.Cqrs
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}