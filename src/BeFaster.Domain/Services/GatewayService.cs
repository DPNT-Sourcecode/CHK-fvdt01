using BeFaster.Domain.Cqrs;
using MediatR;
using System;
using System.Threading.Tasks;

namespace BeFaster.Domain.Services
{
    public class GatewayService : IGatewayService
    {
        private readonly IMediator _mediator;

        public GatewayService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<CalculateSumResult> CalculateSum(CalculateSumCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
    }
}
