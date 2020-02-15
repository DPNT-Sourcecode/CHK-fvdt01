

using BeFaster.Core.Data;
using BeFaster.Core.Models;

namespace BeFaster.Domain.Models
{
    public class CartItem : ICartItem
    {
        public IProduct Product { get; set; }
        public IProductOffer Offer { get; set; }
        public int? Quantity { get; set; }
        public int? AvailableQuantity { get; set; }
        public int? Total { get; set; }
        public bool? Processed { get; set; }
    }
}
