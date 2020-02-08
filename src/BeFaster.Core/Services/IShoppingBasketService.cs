using BeFaster.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeFaster.Core.Services
{
    public interface IShoppingBasketService
    {
        Task<int> CalculateItemTotal(dynamic aggregateSku, ISkuItem item);
        Task<int> Checkout(string skus);
        IEnumerable<dynamic> GetSkuAggregates(string skus);
    }
}