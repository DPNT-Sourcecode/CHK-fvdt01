namespace BeFaster.Core.Cqrs
{
    public interface ICalculateSumCommand
    {
        int? Input1 { get; set; }
        int? Input2 { get; set; }
    }
}