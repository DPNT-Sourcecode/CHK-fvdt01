using BeFaster.Core.Data;

namespace BeFaster.Core.Models
{
    public interface ICartItem
    {
        IProduct Product { get; set; }
        int? Quantity { get; set; }
        int? AvailableQuantity { get; set; }
        IProductOffer Offer { get; set; }
        int? Total { get; set; }
        bool? Processed { get; set; }
    }
}