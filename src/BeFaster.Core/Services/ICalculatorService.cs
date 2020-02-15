using System.Threading.Tasks;

namespace BeFaster.Core.Services
{
    public interface ICalculatorService
    {
        Task<int> Add(int param1, int param2);
    }
}