using BeFaster.Core.Cqrs;
using BeFaster.Core.Services;
using BeFaster.Domain.Cqrs.Validators;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BeFaster.Domain.Cqrs
{
    public class HelloCommandHandler : ICommandHandler<HelloCommand, HelloResult>
    {
        private ILogger<HelloCommandHandler> _logger;
        private IMessageService _messageService;
        public HelloCommandHandler(ILogger<HelloCommandHandler> logger,
                                   IMessageService messageService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
        }

        public async Task<HelloResult> Handle(HelloCommand command, CancellationToken cancellationToken = default)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            HelloResult result = null;
            _logger.LogInformation("Hello command request received,{@command}", command);

            var sumCommandValidator = new HelloCommandValidator();
            var validationErrors = sumCommandValidator.Validate(command);

            if (validationErrors.Errors.Any())
            {
                _logger.LogInformation("Validation for hello command failed, {@validationErrors}", validationErrors);
                var errors = validationErrors.Errors.ToDictionary(x => x.ErrorCode, x => x.ErrorMessage);
                result = new HelloResult(errors);
                return result;
            }

            var message = await _messageService.Hello(command.Message);       
            
            result = new HelloResult { Result = message };

            return result;
        }
    }
}
