using BeFaster.Core.Data;
using BeFaster.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeFaster.Domain.Services
{
    public interface IOfferService
    {
        void ApplyOffers(ICart cart, KeyValuePair<string, ICartItem> cartItem);
        Task<IEnumerable<IProductOffer>> GetOffers();
        IEnumerable<IProductOffer> Lookup(string sku);
    }
}