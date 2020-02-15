using BeFaster.Core.Models;

namespace BeFaster.Domain.Models
{
    public class OfferSummaryItem : IOfferSummaryItem
    {
        public IProduct Product { get; set; }
        public IProductOffer Offer { get; set; }
        public int? AtQuantity { get; set; }
        public int? AtPrice { get; set; }
        public int? Total { get; set; }
    }
}
