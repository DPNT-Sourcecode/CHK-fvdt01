using BeFaster.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeFaster.Core.Services
{
    public interface IProductService
    {
        Task<IEnumerable<IProduct>> GetProducts();
        IProduct Lookup(string sku);

    }
}