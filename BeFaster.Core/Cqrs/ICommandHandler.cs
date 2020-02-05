using System.Threading.Tasks;

namespace BeFaster.Core.Cqrs.CommandHandlers
{
    public interface ICommandHandler<in TParameter, TResult>
                 where TParameter : ICommand
                 where TResult : IResult
    {
        Task<TResult> HandleAsync(TParameter command);
    }
}
