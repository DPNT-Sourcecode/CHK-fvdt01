using BeFaster.Core.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BeFaster.Domain.Services
{
    public class MessageService : IMessageService
    {
        private readonly ILogger<MessageService> _logger;
        
        public MessageService(ILogger<MessageService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<string> Hello(string message)
        {
            if(string.IsNullOrEmpty(message))
                return Task.FromResult($"Hello, World!");            
            else
                return Task.FromResult($"Hello, {message}!");
        }
    }
}
