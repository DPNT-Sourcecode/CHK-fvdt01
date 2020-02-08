using BeFaster.Domain;
using BeFaster.Domain.Cqrs;
using BeFaster.Domain.Cqrs.Validators;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace BeFaster.App.Tests.Solutions.HLO
{
    public class HelloCommandValidatorTests
    {
        [Theory]
        [InlineData("", "Hello, World!")]
        public void HelloCommandValidator_ReturnsNoErrors_WhenCommandValid(string message, string expected)
        {
            //arrange
            var command = new HelloCommand {Message= message};

            //act
            var validator = new HelloCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().Be(0);
            result.Should().Equals(expected);
        }

        [Theory]
        [InlineData("John", "Hello, John!")]
        public void HelloCommandValidator_ReturnsNoErrors_WhenMessagePassed(string message, string expected)
        {
            //arrange
            var command = new HelloCommand { Message = message };

            //act
            var validator = new HelloCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().Be(0);
            result.Should().Equals(expected);
        }

        [Theory]
        [InlineData(null)]        
        public void HelloCommandValidator_ReturnsError_WhenParam1Empty(string message)
        {
            //arrange
            var command = new HelloCommand { Message = message };

            //act
            var validator = new HelloCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Equals(1);
            result.Errors.FirstOrDefault().ErrorCode
                                          .Should().Equals(ValidationErrors.MessageIsRequired.Code);
            result.Errors.FirstOrDefault().ErrorMessage
                                          .Should().Equals(ValidationErrors.MessageIsRequired.Message);
        }
    }
}


