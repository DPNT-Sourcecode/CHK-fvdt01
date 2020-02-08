using BeFaster.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeFaster.Core.Services
{
    public interface IShoppingBasketService
    {
        int CalculateItemTotal(dynamic aggregateSku, ISkuItem sku);
        Task<int> Checkout(string skus);
        IEnumerable<dynamic> GetSkuAggregates(string skus);
    }
}