using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeFaster.Core.Data
{
    public interface ISkuRepository
    {
        Task<List<ISkuItem>> GetAll();
    }
}