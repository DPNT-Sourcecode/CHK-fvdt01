using BeFaster.Core.Data;
using System.Threading.Tasks;

namespace BeFaster.Core.Builders
{
    public interface IOfferBuilder
    {
        Task<ICompositeOffer> Build(string speciaOfferDsl);
        Task<ICompositeOffer> BuildBuyOffer(string speciaOfferDsl);
        Task<ICompositeOffer> BuildFreeOffer(string speciaOfferDsl);
    }
}