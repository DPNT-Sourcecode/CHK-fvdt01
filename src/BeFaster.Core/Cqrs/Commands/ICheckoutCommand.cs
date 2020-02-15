namespace BeFaster.Domain.Cqrs
{
    public interface ICheckoutCommand
    {
        string Skus { get; set; }
    }
}