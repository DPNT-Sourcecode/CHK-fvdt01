using BeFaster.Core.Services;
using BeFaster.Domain.Cqrs;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BeFaster.App.Tests.Solutions.HLO
{
    public class HelloCommandHandlerTests
    {
        [Fact]
        public void HelloCommandHandlerContructor_ThrowsArgumentException_WhenLoggerNull()
        {
            //arrange
            var messageService  = Substitute.For<IMessageService>();

            //act
            Action action = () => new HelloCommandHandler(null, messageService);
            
            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void HelloCommandHandlerContructor_ThrowsArgumentNullException_WhenMessageServiceNull()
        {
            //arrange
            var logger = Substitute.For<ILogger<HelloCommandHandler>>();

            //act
            Action action = () => new HelloCommandHandler(logger, null);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void HelloCommandHandlerHandle_ThrowsArgumentNullException_WhenCommandNull()
        {
            //arrange
            var logger = Substitute.For<ILogger<HelloCommandHandler>>();
            var messageService = Substitute.For<IMessageService>();

            //act
            var commandHandler = new HelloCommandHandler(logger, messageService);            
            Func<Task> action = async () => await commandHandler.Handle(null,default);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void HelloCommandHandlerHandle_ReturnsResult_WithValidCommand()
        {
            //arrange
            var logger = Substitute.For<ILogger<HelloCommandHandler>>();            
            var messageService = Substitute.For<IMessageService>();
            var command = new HelloCommand { Message="Hi there"};
            var commandHandler = new HelloCommandHandler(logger, messageService);

            //act
            Action action = async () => await commandHandler.Handle(command, default);

            //assert
            action.Should().Equals("Hi there");
        }
    }
}

