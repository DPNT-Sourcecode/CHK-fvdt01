

using BeFaster.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeFaster.Data
{
    public class OfferRepositoryInMemory : IOfferRepository
    {
        private List<IOffer> _items;
        public OfferRepositoryInMemory()
        {
            _items = new List<IOffer>();
            _items.Add(new Offer { OfferId = Guid.NewGuid(), OfferDSL = "3A for 130" });
            _items.Add(new Offer { OfferId = Guid.NewGuid(), OfferDSL = "5A for 200" });
            _items.Add(new Offer { OfferId = Guid.NewGuid(), OfferDSL = "2B for 45" });
            _items.Add(new Offer { OfferId = Guid.NewGuid(), OfferDSL = "2E get one B free" });
        }

        public Task<List<IOffer>> GetAll()
        {
            return Task.FromResult<List<IOffer>>(_items);
        }
    }
}
