

using BeFaster.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeFaster.Data
{
    public class SkuRepositoryInMemory : ISkuRepository
    {
        private List<ISkuItem> _items;
        public SkuRepositoryInMemory()
        {
            _items = new List<ISkuItem>();
            _items.Add(new SkuItem { SKU = "A", Price = 50, SpecialOffer = new SpecialOffer { Sku="A", Price=130, Quantity=3 } });
            _items.Add(new SkuItem { SKU = "B", Price = 30, SpecialOffer = new SpecialOffer { Sku = "B", Price =45, Quantity =2 } });
            _items.Add(new SkuItem { SKU = "C", Price = 20, SpecialOffer = null });
            _items.Add(new SkuItem { SKU = "D", Price = 15, SpecialOffer = null });
        }
        public Task<List<ISkuItem>> GetAll()
        {
            return Task.FromResult<List<ISkuItem>>(_items);
        }
    }
}
