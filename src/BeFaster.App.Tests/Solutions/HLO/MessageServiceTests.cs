using BeFaster.Core.Services;
using BeFaster.Domain.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using Xunit;

namespace BeFaster.App.Tests.Solutions.HLO
{
    public class MessageServiceTests
    {
        [Fact]
        public void MessageServiceContructor_ThrowsArgumentException_WhenLoggerNull()
        {
            //arrange
            ILogger<MessageService> logger = null;
            var messageService  = Substitute.For<IMessageService>();
            
            //act
            Action action = () => new MessageService(logger);
            
            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("John", "Hello John")]
        public void MessageServiceHello_ReturnsResult_WithValidParams(string message, string expected)
        {
            //arrange
            var logger = Substitute.For<ILogger<MessageService>>();
            
            //act
            var messageService = new MessageService(logger);           
            Action action = async () => await messageService.Hello(message);

            //assert
            action.Should().Equals(expected);
        }
    }
}
