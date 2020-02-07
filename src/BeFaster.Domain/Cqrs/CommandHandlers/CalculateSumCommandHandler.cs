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
    public class CalculateSumCommandHandler : ICommandHandler<CalculateSumCommand, CalculateSumResult>
    {
        private ILogger<CalculateSumCommandHandler> _logger;
        private ICalculatorService _calculatorService;
        public CalculateSumCommandHandler(ILogger<CalculateSumCommandHandler> logger,
                                          ICalculatorService calculatorService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _calculatorService = calculatorService ?? throw new ArgumentNullException(nameof(calculatorService));
        }

        public async Task<CalculateSumResult> Handle(CalculateSumCommand command, CancellationToken cancellationToken = default)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            CalculateSumResult result = null;
            _logger.LogInformation("CalculateSumCommand request received,{@command}", command);

            var sumCommandValidator = new CalculateSumCommandValidator();
            var validationErrors = sumCommandValidator.Validate(command);

            if (validationErrors.Errors.Any())
            {
                _logger.LogInformation("Validation for calculate sum failed, {@validationErrors}", validationErrors);
                var errors = validationErrors.Errors.ToDictionary(x => x.ErrorCode, x => x.ErrorMessage);
                result = new CalculateSumResult(errors);
                return result;
            }

            var total = await _calculatorService.Add(command.Input1.Value, 
                                                     command.Input2.Value);       
            
            result = new CalculateSumResult { Result = total};

            return result;
        }
    }
}
