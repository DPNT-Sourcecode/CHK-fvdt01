
using System.Threading.Tasks;

namespace BeFaster.Core
{
    public interface IBeFasterService
    {
        Task<int> Sum(ISumCommand command);
    }
}
