using BeFaster.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BeFaster.Domain.Cqrs.CommandHandlers
{
    public class SumCommandHandler : CommandHandler<SumCommand, SumCommandResult>
    {
        private readonly IBeFasterService _beFasterService;

        public SumCommandHandler(ILogger logger,
                                 IBeFasterService beFasterService) : base(logger)
        {
            _beFasterService = beFasterService ?? throw new ArgumentNullException(nameof(beFasterService));
        }
        protected override Task<SumCommandResult> ProcessCommandAsync(SumCommand request)
        {
            throw new NotImplementedException();
        }
    }
}

