
using System.Threading.Tasks;

namespace BeFaster.Core
{
    public interface ISumService
    {
        Task<int> Add(int x, int y);
    }
}

