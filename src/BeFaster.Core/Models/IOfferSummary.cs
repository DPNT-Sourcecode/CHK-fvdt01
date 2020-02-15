using BeFaster.Core.Models;

namespace BeFaster.Core.Models
{
    public interface IOfferSummaryItem
    {
        IProduct Product { get; set; }
        IProductOffer Offer { get; set; }
        int? AtPrice { get; set; }
        int? AtQuantity { get; set; }
        int? Total { get; set; }
    }
}