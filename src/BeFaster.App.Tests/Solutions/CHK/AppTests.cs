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
        [InlineData("ABCDEABCDE", 280)]
        public void App_CheckoutReturnsResult_When3A3B2EOffersApplied(string skus, int expected)
        {
            var result = CheckoutSolution.Checkout(skus);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("BB", 45)]
        public void App_CheckoutReturnsResult_WhenBOfferApplied(string skus, int expected)
        {
            var result = CheckoutSolution.Checkout(skus);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("AAA", 130)]
        [InlineData("AAAA", 180)]
        [InlineData("AAAAA", 200)]
        public void App_ACombinations_ReturnsResult(string skus, int expected)
        {
            var result = CheckoutSolution.Checkout(skus);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("EEEEBB", 190)]
        [InlineData("BEBEEE", 190)]
        [InlineData("EEEB", 120)]
        [InlineData("EEB", 80)]
        public void App_EBCombinations_ReturnsResult(string skus, int expected)
        {
            var result = CheckoutSolution.Checkout(skus);

            Assert.Equal(expected, result);
        }
    }
}
