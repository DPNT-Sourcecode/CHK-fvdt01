

using BeFaster.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeFaster.Data
{
    public class SpecialOfferRepositoryInMemory : ISpecialOfferRepository
    {
        private List<ISpecialOffer> _items;
        public SpecialOfferRepositoryInMemory()
        {
            _items = new List<ISpecialOffer>();
            _items.Add(new SpecialOffer { Sku="A", Price=130, Quantity=3 });
            _items.Add(new SpecialOffer { Sku = "A", Price = 200, Quantity = 5 });
            _items.Add(new SpecialOffer { Sku= "B", Price =45, Quantity =2 });
        }
        public Task<List<ISpecialOffer>> GetAll()
        {
            return Task.FromResult<List<ISpecialOffer>>(_items);
        }
    }
}
