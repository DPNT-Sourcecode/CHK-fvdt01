using BeFaster.Core.Data;
using BeFaster.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeFaster.Domain.Services
{
    public interface IOfferService
    {
        Task<IEnumerable<ICompositeOffer>> GetOffers();
        IEnumerable<ICompositeOffer> Lookup(string sku);
    }
}