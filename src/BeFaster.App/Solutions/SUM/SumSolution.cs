using BeFaster.Domain.Cqrs;
using BeFaster.Domain.Services;
using System;
using System.Linq;

namespace BeFaster.App.Solutions.SUM
{
    public static class SumSolution
    {
        public static int Sum(int x, int y)
        {
            var runtime = new Runtime();
            var service= runtime.GetInstance<IGatewayService>();

            var command = new CalculateSumCommand { Param1 = x, Param2 = y };
            var calculateSumResult = service.CalculateSum(command).Result;

            if (calculateSumResult.HasErrors)
            {
                var error = calculateSumResult.Errors.ToList().FirstOrDefault();
                throw new Exception($"{error.Key}:{error.Value}");
            }
            return calculateSumResult.Result;
        }
    }
}
