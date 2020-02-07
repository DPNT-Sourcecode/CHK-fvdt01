
using BeFaster.Domain.Cqrs;
using System.Threading.Tasks;

namespace BeFaster.Domain.Services
{
    public interface IGatewayService
    {
        Task<CalculateSumResult> CalculateSum(CalculateSumCommand command);
    }
}
