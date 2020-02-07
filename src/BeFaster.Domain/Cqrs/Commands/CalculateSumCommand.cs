using BeFaster.Core.Cqrs;

namespace BeFaster.Domain.Cqrs
{
    public class CalculateSumCommand : ICalculateSumCommand, ICommand<CalculateSumResult>
    {
        public int? Input1 { get; set; }
        public int? Input2 { get; set; }
    }
}