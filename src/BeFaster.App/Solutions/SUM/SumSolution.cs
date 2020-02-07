using BeFaster.Domain.Cqrs;
using BeFaster.Domain.Services;
using System.Threading.Tasks;

namespace BeFaster.App.Solutions.SUM
{
    public static class SumSolution
    {
        public static int Sum(int x, int y)
        {
            var runtime = new Runtime();
            var service= runtime.GetInstance<IBeFasterService>();

            var command = new CalculateSumCommand { Input1 = x, Input2 = y };
            var calculateSumResult = service.Sum(command).Result;
            
            return calculateSumResult.Result;
        }
    }
}
