using BeFaster.Core.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BeFaster.Domain.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ILogger<CheckoutService> _logger;        
        
        public CheckoutService(ILogger<CheckoutService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<int> Checkout(string skus)
        {
            
        }

        
    }
}
