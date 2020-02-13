

using BeFaster.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeFaster.Data
{
    public class ProductRepositoryInMemory : IProductRepository
    {
        private List<IProduct> _items;
        public ProductRepositoryInMemory()
        {
            _items = new List<IProduct>();
            _items.Add(new Product { Sku = "A", Price = 50});
            _items.Add(new Product { Sku = "B", Price = 30});
            _items.Add(new Product { Sku = "C", Price = 20});
            _items.Add(new Product { Sku = "D", Price = 15});
            _items.Add(new Product { Sku = "E", Price = 40 });
        }
        public Task<List<IProduct>> GetAll()
        {
            return Task.FromResult<List<IProduct>>(_items);
        }
    }
}
