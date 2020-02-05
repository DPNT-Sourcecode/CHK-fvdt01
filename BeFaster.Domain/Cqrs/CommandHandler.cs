using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeFaster.Domain.Cqrs
{
    public abstract class CommandHandler<TRequest, TResult> : ICommandHandler<TRequest, TResult>
           where TRequest : ICommand
           where TResult : IResult, new()
    {
        protected ILogger _logger;

        protected CommandHandler(ILogger logger)
        {
            _logger = logger;
        }


        public async Task<TResult> Handle(TRequest command)
        {
            var _stopWatch = new Stopwatch();
            _stopWatch.Start();

            TResult result;

            try
            {
                result = await ProcessCommandAsync(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), $"Error in {0} CommandHandler. Message: {1} \n Stacktrace: {2}", typeof(TRequest).Name, ex.Message, ex.StackTrace);
                throw ex;
            }
            finally
            {
                _stopWatch.Stop();
                _logger.LogDebug($"Response for {0}, elapsed time: {1} msec)", typeof(TRequest).Name, _stopWatch.ElapsedMilliseconds);
            }

            return result;
        }

        public async Task<TResult> HandleAsync(TRequest command)
        {
            var _stopWatch = new Stopwatch();
            _stopWatch.Start();

            Task<TResult> result;

            try
            {
                result = ProcessCommandAsync(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {0} CommandHandler. Message: {1} \n Stacktrace: {2}", typeof(TRequest).Name, ex.Message, ex.StackTrace);
                throw ex;
            }
            finally
            {
                _stopWatch.Stop();
                _logger.LogDebug($"Response for {0}, elapsed time: {1} msec)", typeof(TRequest).Name, _stopWatch.ElapsedMilliseconds);
            }

            return await result;
        }

        protected abstract Task<TResult> ProcessCommandAsync(TRequest request);
    }
}