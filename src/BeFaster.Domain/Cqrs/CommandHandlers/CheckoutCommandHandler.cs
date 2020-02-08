using BeFaster.Core.Cqrs;
using BeFaster.Core.Data;
using BeFaster.Core.Services;
using BeFaster.Domain.Cqrs.Validators;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BeFaster.Domain.Cqrs
{
    public class CheckoutCommandHandler : ICommandHandler<CheckoutCommand, CheckoutResult>
    {
        private ILogger<CheckoutCommandHandler> _logger;
        private ISkuRepository _skuRepository;
        private IShoppingBasketService _shoppingBasketService;
        public CheckoutCommandHandler(ILogger<CheckoutCommandHandler> logger,
                                      ISkuRepository skuRepository,
                                      IShoppingBasketService shoppingBasketService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _skuRepository = skuRepository ?? throw new ArgumentNullException(nameof(skuRepository));
            _shoppingBasketService = shoppingBasketService ?? throw new ArgumentNullException(nameof(shoppingBasketService));
        }

        public async Task<CheckoutResult> Handle(CheckoutCommand command, CancellationToken cancellationToken = default)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            CheckoutResult result = null;
            _logger.LogInformation("Checkout command request received,{@command}", command);

            var checkoutCommandValidator = new CheckoutCommandValidator(_skuRepository);
            var validationErrors = checkoutCommandValidator.Validate(command);

            if (validationErrors.Errors.Any())
            {                
                _logger.LogInformation("Validation for checkout command failed, {@validationErrors}", validationErrors);
                var errors = validationErrors.Errors.ToDictionary(x => x.ErrorCode, x => x.ErrorMessage);
                result = new CheckoutResult(errors);
                result.Result = -1;
                return result;
            }

            var checkoutResult = await _shoppingBasketService.Checkout(command.Skus);       
            
            result = new CheckoutResult { Result = checkoutResult };

            return result;
        }
    }
}
