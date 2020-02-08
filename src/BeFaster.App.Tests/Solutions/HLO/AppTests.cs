using BeFaster.App.Solutions.HLO;
using BeFaster.Domain;
using FluentAssertions;
using System;
using Xunit;


namespace BeFaster.App.Tests.Solutions.HLO
{
    public class AppTests
    {
        [Theory]
        [InlineData("", "Hello, World!")]
        public void App_HelloReturnsResult_WhenEmptyString(string message, string expected)
        {
            var result = HelloSolution.Hello(message);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("John", "Hello, John!")]
        public void App_HelloReturnsResult_WhenValidParams(string message, string expected)
        {
            var result = HelloSolution.Hello(message);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(null)]
        public void App_HelloThrowsException_WhenMessageEmpty(string message)
        {
            //act
            Action action = () => HelloSolution.Hello(message);

            //assert
            action.Should().Throw<Exception>($"{ValidationErrors.MessageIsRequired.Code}:{ValidationErrors.MessageIsRequired.Message}");
        }
    }
}


