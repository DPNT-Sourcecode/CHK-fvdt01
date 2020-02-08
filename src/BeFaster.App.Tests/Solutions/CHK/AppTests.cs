using BeFaster.App.Solutions.CHK;
using Xunit;


namespace BeFaster.App.Tests.Solutions.CHK
{
    public class AppTests
    {
        [Theory]
        [InlineData("ABCD", 115)]
        public void App_CheckoutReturnsResult_WithValidSkus(string skus, int expected)
        {
            var result = CheckoutSolution.Checkout(skus);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("ZZZZ", -1)]
        public void App_CheckoutReturnsError_WithInvalidSkus(string skus, int expected)
        {
            var result = CheckoutSolution.Checkout(skus);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("AAA", 130)]
        public void App_CheckoutReturnsResult_WhenValidSpecialOfferApplied(string skus, int expected)
        {
            var result = CheckoutSolution.Checkout(skus);

            Assert.Equal(expected, result);
        }
    }
}
