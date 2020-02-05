using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BeFaster.Domain.Cqrs.CommandHandlers
{
    public class SumCommandHandler : CommandHandler<SumCommand, SumCommandResult>
    {
        protected ILogger _logger;
        public SumCommandHandler(ILogger logger) 
        {
            _logger = logger?? new ArgumentNullException(nameof(logger));
        }
        protected override Task<SumCommandResult> ProcessCommandAsync(SumCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
