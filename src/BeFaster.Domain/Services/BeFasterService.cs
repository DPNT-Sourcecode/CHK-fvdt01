using BeFaster.Domain.Cqrs;
using MediatR;
using System;
using System.Threading.Tasks;

namespace BeFaster.Domain.Services
{
    public class BeFasterService : IBeFasterService
    {
        private readonly IMediator _mediator;

        public BeFasterService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<CalculateSumResult> Sum(CalculateSumCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
    }
}
