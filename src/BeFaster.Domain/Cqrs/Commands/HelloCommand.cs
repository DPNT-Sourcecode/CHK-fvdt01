using BeFaster.Core.Cqrs;

namespace BeFaster.Domain.Cqrs
{
    public class HelloCommand : IHelloCommand, ICommand<HelloResult>
    {
        public string Message { get; set; }
    }
}