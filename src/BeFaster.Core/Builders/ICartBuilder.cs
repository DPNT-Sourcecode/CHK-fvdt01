using BeFaster.Core.Data;
using BeFaster.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeFaster.Core.Builders
{
    public interface ICartBuilder
    {
        Task<ICart> Build(string skus);
    }
}