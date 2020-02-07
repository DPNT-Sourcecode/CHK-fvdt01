using BeFaster.Core.Services;
using BeFaster.Domain.Cqrs;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BeFaster.App.Tests.Solutions.SUM
{
    public class SumCommandHandlerTests
    {
        [Fact]
        public void CalculateSumCommandHandlerContructor_ThrowsArgumentException_WhenLoggerNull()
        {
            //arrange
            ILogger<CalculateSumCommandHandler> logger = null;

            //act
            var calculatorService  = Substitute.For<ICalculatorService>();
            Action action = () => new CalculateSumCommandHandler(logger, calculatorService);
            
            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CalculateSumCommandHandlerContructor_ThrowsArgumentNullException_WhenCalculatorServiceNull()
        {
            //arrange
            ILogger<CalculateSumCommandHandler> logger = null;
            
            //act
            var calculatorService = Substitute.For<ICalculatorService>();
            Action action = () => new CalculateSumCommandHandler(logger, calculatorService);
            
            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CalculateSumCommandHandlerHandle_ThrowsArgumentNullException_WhenCommandNull()
        {
            //arrange
            var logger = Substitute.For<ILogger<CalculateSumCommandHandler>>();
            var calculatorService = Substitute.For<ICalculatorService>();

            //act
            var commandHandler = new CalculateSumCommandHandler(logger, calculatorService);            
            Func<Task> action = async () => await commandHandler.Handle(null,default);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CalculateSumCommandHandlerHandle_ReturnsResult_WithValidCommand()
        {
            //arrange
            var logger = Substitute.For<ILogger<CalculateSumCommandHandler>>();            
            var calculatorService = Substitute.For<ICalculatorService>();
            var command = new CalculateSumCommand { Input1 = 2, Input2 = 1 };
            var commandHandler = new CalculateSumCommandHandler(logger, calculatorService);

            //act
            Action action = async () => await commandHandler.Handle(command, default);

            //assert
            action.Should().Equals(3);
        }
    }
}
