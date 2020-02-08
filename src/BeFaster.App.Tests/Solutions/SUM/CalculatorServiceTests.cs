using BeFaster.Core.Services;
using BeFaster.Domain.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using Xunit;

namespace BeFaster.App.Tests.Solutions.SUM
{
    public class CalculatorServiceTests
    {
        [Fact]
        public void CalculatorServiceContructor_ThrowsArgumentException_WhenLoggerNull()
        {
            //arrange
            ILogger<CalculatorService> logger = null;
            var calculatorService  = Substitute.For<ICalculatorService>();
            
            //act
            Action action = () => new CalculatorService(logger);
            
            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CalculatorServiceAdd_ReturnsResult_WithValidParams()
        {
            //arrange
            var logger = Substitute.For<ILogger<CalculatorService>>();
            
            //act
            var calculatorService = new CalculatorService(logger);           
            Action action = async () => await calculatorService.Add(1,1);

            //assert
            action.Should().Equals(2);
        }
    }
}
