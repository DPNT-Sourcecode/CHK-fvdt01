using BeFaster.Core.Factories;
using BeFaster.Core.Services;
using BeFaster.Domain.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using Xunit;

namespace BeFaster.App.Tests.Solutions.CHK
{
    public class CartServiceTests
    {
        [Fact]
        public void CartServiceContructor_ThrowsArgumentException_WhenLoggerNull()
        {
            //arrange            
            var speciaOfferService = Substitute.For<IOfferService>();
            var skuService = Substitute.For<IProductService>();
            var cartItemBuilder = Substitute.For<ICartFactory>();

            //act
            Action action = () => new CartService(null, skuService, speciaOfferService, cartItemBuilder);
            
            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CartServiceContructor_ThrowsArgumentException_WhenProductServiceNull()
        {
            //arrange            
            var logger = Substitute.For<ILogger<CartService>>();
            var speciaOfferService = Substitute.For<IOfferService>();
            var skuService = Substitute.For<IProductService>();
            var cartItemBuilder = Substitute.For<ICartFactory>();

            //act
            Action action = () => new CartService(logger, null, speciaOfferService, cartItemBuilder);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CartServiceContructor_ThrowsArgumentException_WhenOfferServiceNull()
        {
            //arrange            
            var logger = Substitute.For<ILogger<CartService>>();
            var speciaOfferService = Substitute.For<IOfferService>();
            var skuService = Substitute.For<IProductService>();
            var cartItemBuilder = Substitute.For<ICartFactory>();

            //act
            Action action = () => new CartService(logger, skuService, null, cartItemBuilder);


            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CartServiceContructor_ThrowsArgumentException_WhenAggregateBuilderNull()
        {
            //arrange            
            var logger = Substitute.For<ILogger<CartService>>();
            var speciaOfferService = Substitute.For<IOfferService>();
            var skuService = Substitute.For<IProductService>();
            var cartItemBuilder = Substitute.For<ICartFactory>();

            //act
            Action action = () => new CartService(logger, skuService, speciaOfferService, null);


            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("ABCD", 115)]
        public void CartService_CalculateItemTotalReturnsResult_WhenWithValidSkus(string skus, int expected)
        {
            //arrange
            var logger = Substitute.For<ILogger<CartService>>();
            var speciaOfferService = Substitute.For<IOfferService>();
            var skuService = Substitute.For<IProductService>();
            var cartItemBuilder = Substitute.For<ICartFactory>();

            //act
            var service = new CartService(logger, skuService, speciaOfferService, cartItemBuilder);
            Action action = async () => await service.Checkout(skus);

            //assert
            action.Should().Equals(expected);
        }

        [Theory]
        [InlineData("aaaaaaa", 310)]
        public void CartService_CalculateItemTotalReturnsResult_WhenMultipleOffersApply(string skus, int expected)
        {
            //arrange
            var logger = Substitute.For<ILogger<CartService>>();
            var speciaOfferService = Substitute.For<IOfferService>();
            var skuService = Substitute.For<IProductService>();
            var cartItemBuilder = Substitute.For<ICartFactory>();

            //act
            var service = new CartService(logger, skuService, speciaOfferService, cartItemBuilder);
            Action action = async () => await service.Checkout(skus);

            //assert
            action.Should().Equals(expected);
        }

        [Theory]
        [InlineData("BB", 45)]
        public void CartService_CalculateItemTotalReturnsResult_WhenBBOffersApply(string skus, int expected)
        {
            //arrange
            var logger = Substitute.For<ILogger<CartService>>();
            var speciaOfferService = Substitute.For<IOfferService>();
            var skuService = Substitute.For<IProductService>();
            var cartItemBuilder = Substitute.For<ICartFactory>();

            //act
            var service = new CartService(logger, skuService, speciaOfferService, cartItemBuilder);
            Action action = async () => await service.Checkout(skus);

            //assert
            action.Should().Equals(expected);
        }

        [Theory]
        [InlineData("AAAAAAAA", 330)]
        public void CartService_CalculateItemTotalReturnsResult_When3A5AOffersApply(string skus, int expected)
        {
            //arrange
            var logger = Substitute.For<ILogger<CartService>>();
            var speciaOfferService = Substitute.For<IOfferService>();
            var skuService = Substitute.For<IProductService>();
            var cartItemBuilder = Substitute.For<ICartFactory>();

            //act
            var service = new CartService(logger, skuService, speciaOfferService, cartItemBuilder);
            Action action = async () => await service.Checkout(skus);

            //assert
            action.Should().Equals(expected);
        }
    }
}
