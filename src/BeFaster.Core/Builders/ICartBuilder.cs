using BeFaster.Core.Data;
using BeFaster.Core.Models;
using System.Threading.Tasks;

namespace BeFaster.Core.Factories
{
    public interface ICartFactory
    {
        Task<ICart> Create(string skus);
    }
}