using BeFaster.Core.Cqrs;

namespace BeFaster.Domain.Cqrs
{
    public class CheckoutCommand : ICheckoutCommand, ICommand<CheckoutResult>
    {
        public string Skus { get; set; }
    }
}