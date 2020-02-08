using BeFaster.Domain.Cqrs;
using BeFaster.Domain.Services;
using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        public static int ComputePrice(string skus)
        {
            throw new SolutionNotImplementedException();
        }

        public static int Checkout(string skus)
        {
            var runtime = new Runtime();
            var service = runtime.GetInstance<IGatewayService>();

            var command = new CheckoutCommand { Skus = skus };
            var checkoutResult = service.Checkout(command).Result;

            return checkoutResult.Result;
        }
    }
}
