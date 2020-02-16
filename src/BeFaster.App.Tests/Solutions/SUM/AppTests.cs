using BeFaster.App.Solutions.SUM;
using BeFaster.Domain;
using FluentAssertions;
using System;
using Xunit;


namespace BeFaster.App.Tests.Solutions.SUM
{
    public class AppTests
    {
        [Theory]
        [InlineData(1, 2, 3)]
        public void App_SumReturnsResult_WithValidParams(int x, int y, int expected)
        {
            var result = SumSolution.Sum(x, y);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        public void App_SumReturnsResult_WithMinAndMaxBounds(int x, int y, int expected)
        {
            var result = SumSolution.Sum(x, y);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-1, 100)]
        public void App_SumThrowsException_WhenXIsLessThanZero(int x, int y)
        {
            //act
            Action action = () => SumSolution.Sum(x, y);

            //assert
            action.Should().Throw<Exception>($"{ValidationErrors.Param1IsInvalid.Code}:{ValidationErrors.Param1IsInvalid.Message}");
        }

        [Theory]
        [InlineData(1, -100)]
        public void App_SumThrowsException_WhenYIsLessThanZero(int x, int y)
        {
            //act
            Action action = () => SumSolution.Sum(x, y);

            //assert            
            action.Should().Throw<Exception>($"{ValidationErrors.Param2IsInvalid.Code}:{ValidationErrors.Param2IsInvalid.Message}");
        }
    }
}
