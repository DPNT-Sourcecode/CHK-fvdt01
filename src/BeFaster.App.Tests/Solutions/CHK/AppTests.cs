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

        [Theory]
        [InlineData("AAAAAAAAA", 380)]
        public void App_CheckoutReturnsResult_When3A5ASpecialOffersApplied(string skus, int expected)
        {
            var result = CheckoutSolution.Checkout(skus);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("AAABBBEE", 255)]
        public void App_CheckoutReturnsResult_When3A3B2ESpecialOffersApplied(string skus, int expected)
        {
            var result = CheckoutSolution.Checkout(skus);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("BB", 45)]
        public void App_CheckoutReturnsResult_WhenBSpecialOfferApplied(string skus, int expected)
        {
            var result = CheckoutSolution.Checkout(skus);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("AAAAA", 200)]
        public void App_CheckoutReturnsResult_When5ASpecialOfferApplied(string skus, int expected)
        {
            var result = CheckoutSolution.Checkout(skus);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("AAAAAAAAAA", 400)]
        public void App_CheckoutReturnsResult_When10ASpecialOfferApplied(string skus, int expected)
        {
            var result = CheckoutSolution.Checkout(skus);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("EEEEBB", 160)]
        [InlineData("BEBEEE", 160)]
        public void App_CheckoutReturnsResult_When4E2BSpecialOfferApplied(string skus, int expected)
        {
            var result = CheckoutSolution.Checkout(skus);

            Assert.Equal(expected, result);
        }
    }
}
