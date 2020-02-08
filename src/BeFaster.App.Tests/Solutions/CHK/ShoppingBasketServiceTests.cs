using BeFaster.Core.Data;
using BeFaster.Domain.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using Xunit;

namespace BeFaster.App.Tests.Solutions.CHK
{
    public class ShoppingBasketServiceTests
    {
        [Fact]
        public void ShoppingBasketServiceContructor_ThrowsArgumentException_WhenLoggerNull()
        {
            //arrange            
            var respository = Substitute.For<ISkuRepository>();
            
            //act
            Action action = () => new ShoppingBasketService(null, respository);
            
            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ShoppingBasketServiceContructor_ThrowsArgumentException_WhenRepositoryNull()
        {
            //arrange            
            var logger = Substitute.For<ILogger<ShoppingBasketService>>();

            //act
            Action action = () => new ShoppingBasketService(logger, null);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("ABCD", 115)]
        public void ShoppingBasketService_CalculateItemTotalReturnsResult_WhenWithValidSkus(string skus, int expected)
        {
            //arrange
            var logger = Substitute.For<ILogger<ShoppingBasketService>>();
            var repository = Substitute.For<ISkuRepository>();

            //act
            var service = new ShoppingBasketService(logger, repository);           
            Action action = async () => await service.Checkout(skus);

            //assert
            action.Should().Equals(expected);
        }
    }
}
