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
            var skuRepository = Substitute.For<ISkuRepository>();
            var specialOfferRepository = Substitute.For<ISpecialOfferRepository>();

            //act
            Action action = () => new ShoppingBasketService(null, skuRepository, specialOfferRepository);
            
            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ShoppingBasketServiceContructor_ThrowsArgumentException_WhenSkuRepositoryNull()
        {
            //arrange            
            var logger = Substitute.For<ILogger<ShoppingBasketService>>();
            var skuRepository = Substitute.For<ISkuRepository>();
            var specialOfferRepository = Substitute.For<ISpecialOfferRepository>();

            //act
            Action action = () => new ShoppingBasketService(logger, null, specialOfferRepository);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ShoppingBasketServiceContructor_ThrowsArgumentException_WhenSpecialOfferRepositoryNull()
        {
            //arrange            
            var logger = Substitute.For<ILogger<ShoppingBasketService>>();
            var skuRepository = Substitute.For<ISkuRepository>();
            
            //act
            Action action = () => new ShoppingBasketService(logger, skuRepository, null);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("ABCD", 115)]
        public void ShoppingBasketService_CalculateItemTotalReturnsResult_WhenWithValidSkus(string skus, int expected)
        {
            //arrange
            var logger = Substitute.For<ILogger<ShoppingBasketService>>();
            var skuRepository = Substitute.For<ISkuRepository>();
            var specialOfferRepository = Substitute.For<ISpecialOfferRepository>();

            //act
            var service = new ShoppingBasketService(logger, skuRepository, specialOfferRepository);           
            Action action = async () => await service.Checkout(skus);

            //assert
            action.Should().Equals(expected);
        }

        [Theory]
        [InlineData("aaaaaaa", 310)]
        public void ShoppingBasketService_CalculateItemTotalReturnsResult_WhenMultipleSpecialOffersApply(string skus, int expected)
        {
            //arrange
            var logger = Substitute.For<ILogger<ShoppingBasketService>>();
            var skuRepository = Substitute.For<ISkuRepository>();
            var specialOfferRepository = Substitute.For<ISpecialOfferRepository>();

            //act
            var service = new ShoppingBasketService(logger, skuRepository, specialOfferRepository);
            Action action = async () => await service.Checkout(skus);

            //assert
            action.Should().Equals(expected);
        }

        [Theory]
        [InlineData("BB", 45)]
        public void ShoppingBasketService_CalculateItemTotalReturnsResult_WhenBBSpecialOffersApply(string skus, int expected)
        {
            //arrange
            var logger = Substitute.For<ILogger<ShoppingBasketService>>();
            var skuRepository = Substitute.For<ISkuRepository>();
            var specialOfferRepository = Substitute.For<ISpecialOfferRepository>();

            //act
            var service = new ShoppingBasketService(logger, skuRepository, specialOfferRepository);
            Action action = async () => await service.Checkout(skus);

            //assert
            action.Should().Equals(expected);
        }

        [Theory]
        [InlineData("AAAAAAAA", 330)]
        public void ShoppingBasketService_CalculateItemTotalReturnsResult_When3A5ASpecialOffersApply(string skus, int expected)
        {
            //arrange
            var logger = Substitute.For<ILogger<ShoppingBasketService>>();
            var skuRepository = Substitute.For<ISkuRepository>();
            var specialOfferRepository = Substitute.For<ISpecialOfferRepository>();

            //act
            var service = new ShoppingBasketService(logger, skuRepository, specialOfferRepository);
            Action action = async () => await service.Checkout(skus);

            //assert
            action.Should().Equals(expected);
        }
    }
}
