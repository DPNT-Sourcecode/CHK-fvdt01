namespace BeFaster.Core.Cqrs
{
    public interface ICalculateSumCommand
    {
        int? Param1 { get; set; }
        int? Param2 { get; set; }
    }
}