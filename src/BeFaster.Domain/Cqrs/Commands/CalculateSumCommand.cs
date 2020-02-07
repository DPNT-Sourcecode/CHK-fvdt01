using BeFaster.Core.Cqrs;

namespace BeFaster.Domain.Cqrs
{
    public class CalculateSumCommand : ICalculateSumCommand, ICommand<CalculateSumResult>
    {
        public int? Param1 { get; set; }
        public int? Param2 { get; set; }
    }
}