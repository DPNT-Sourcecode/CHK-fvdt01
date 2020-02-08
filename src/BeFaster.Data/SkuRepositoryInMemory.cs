

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
            _items.Add(new SkuItem { SKU = "A", Price = 50});
            _items.Add(new SkuItem { SKU = "B", Price = 30});
            _items.Add(new SkuItem { SKU = "C", Price = 20});
            _items.Add(new SkuItem { SKU = "D", Price = 15});
            _items.Add(new SkuItem { SKU = "E", Price = 40 });
        }
        public Task<List<ISkuItem>> GetAll()
        {
            return Task.FromResult<List<ISkuItem>>(_items);
        }
    }
}
