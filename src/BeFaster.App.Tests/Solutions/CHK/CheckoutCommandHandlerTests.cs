using BeFaster.Core.Data;
using BeFaster.Core.Services;
using BeFaster.Domain.Cqrs;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BeFaster.App.Tests.Solutions.CHK
{
    public class CheckoutCommandHandlerTests
    {
        [Fact]
        public void CheckoutCommandHandlerContructor_ThrowsArgumentException_WhenLoggerNull()
        {
            //arrange
            var service  = Substitute.For<IShoppingBasketService>();
            var repository = Substitute.For<ISkuRepository>();

            //act
            Action action = () => new CheckoutCommandHandler(null, repository, service);
            
            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CheckoutCommandHandlerContructor_ThrowsArgumentNullException_WhenShoppingBasketServiceNull()
        {
            //arrange
            var logger = Substitute.For<ILogger<CheckoutCommandHandler>>();
            var repository = Substitute.For<ISkuRepository>();

            //act
            Action action = () => new CheckoutCommandHandler(logger, repository, null);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CheckoutCommandHandlerContructor_ThrowsArgumentNullException_WhenRepositoryNull()
        {
            //arrange
            var logger = Substitute.For<ILogger<CheckoutCommandHandler>>();
            var service = Substitute.For<IShoppingBasketService>();

            //act
            Action action = () => new CheckoutCommandHandler(logger, null, service);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CheckoutCommandHandlerHandle_ThrowsArgumentNullException_WhenCommandNull()
        {
            //arrange
            var logger = Substitute.For<ILogger<CheckoutCommandHandler>>();
            var service = Substitute.For<IShoppingBasketService>();
            var repository = Substitute.For<ISkuRepository>();

            //act
            var commandHandler = new CheckoutCommandHandler(logger, repository, service);            
            Func<Task> action = async () => await commandHandler.Handle(null,default);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("ABCD", 115)]
        public void CheckoutCommandHandlerHandle_ReturnsResult_WithValidCommand(string skus, int expected)
        {
            //arrange
            var logger = Substitute.For<ILogger<CheckoutCommandHandler>>();            
            var service = Substitute.For<IShoppingBasketService>();
            var repository = Substitute.For<ISkuRepository>();
            var command = new CheckoutCommand { Skus=skus};
            var commandHandler = new CheckoutCommandHandler(logger, repository, service);

            //act
            Action action = async () => await commandHandler.Handle(command, default);

            //assert
            action.Should().Equals(expected);
        }

        [Theory]
        [InlineData("AAA", 130)]
        public void CheckoutCommandHandlerHandle_ReturnsResult_WhenSpecialOfferValid(string skus, int expected)
        {
            //arrange
            var logger = Substitute.For<ILogger<CheckoutCommandHandler>>();
            var service = Substitute.For<IShoppingBasketService>();
            var repository = Substitute.For<ISkuRepository>();
            var command = new CheckoutCommand { Skus = skus };
            var commandHandler = new CheckoutCommandHandler(logger, repository, service);

            //act
            Action action = async () => await commandHandler.Handle(command, default);

            //assert
            action.Should().Equals(expected);
        }

        [Theory]
        [InlineData("ZZZ", -1)]
        public void CheckoutCommandHandlerHandle_ReturnsError_WhenSkuInvalid(string skus, int expected)
        {
            //arrange
            var logger = Substitute.For<ILogger<CheckoutCommandHandler>>();
            var service = Substitute.For<IShoppingBasketService>();
            var repository = Substitute.For<ISkuRepository>();
            var command = new CheckoutCommand { Skus = skus };
            var commandHandler = new CheckoutCommandHandler(logger, repository, service);

            //act
            Action action = async () => await commandHandler.Handle(command, default);

            //assert
            action.Should().Equals(expected);
        }
    }
}
