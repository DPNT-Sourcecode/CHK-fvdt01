using BeFaster.Domain.Cqrs;
using BeFaster.Domain.Services;
using BeFaster.Runner.Exceptions;
using System;
using System.Linq;

namespace BeFaster.App.Solutions.HLO
{
    public static class HelloSolution
    {
        public static string Hello(string friendName)
        {
            var runtime = new Runtime();
            var service = runtime.GetInstance<IGatewayService>();

            var command = new HelloCommand { Message=friendName };
            var helloResult = service.Hello(command).Result;

            if (helloResult.HasErrors)
            {
                var error = helloResult.Errors.ToList().FirstOrDefault();
                throw new Exception($"{error.Key}:{error.Value}");
            }
            return helloResult.Result;
        }
    }
}
