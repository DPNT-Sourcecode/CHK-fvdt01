using BeFaster.Core.Models;
using System.Threading.Tasks;

namespace BeFaster.Core.Factories
{
    public interface IOfferFactory
    {
        Task<IProductOffer> Create(string speciaOfferDsl);
        Task<IProductOffer> CreateBuyOffer(string speciaOfferDsl);
        Task<IProductOffer> CreateFreeOffer(string speciaOfferDsl);
    }
}