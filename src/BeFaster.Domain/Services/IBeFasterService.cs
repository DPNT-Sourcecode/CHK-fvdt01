
using BeFaster.Domain.Cqrs;
using System.Threading.Tasks;

namespace BeFaster.Domain.Services
{
    public interface IBeFasterService
    {
        Task<CalculateSumResult> SumAsync(CalculateSumCommand command);
    }
}
