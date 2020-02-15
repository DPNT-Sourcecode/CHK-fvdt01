using BeFaster.Core.Enums;
using System.Collections.Generic;

namespace BeFaster.Core.Models
{
    public interface IProductOffer : IOffer
    {
        IProduct Product { get; set; }
        int? AtOfferQuantity { get; set; }
        int? AtOfferPrice { get; set; }
        int? FreeOfferQuantity { get; set; }
        IProduct FreeOfferProduct { get; set; }
        ICart Cart { get; set; }
        OfferType OfferType { get; set; }
        void Apply(KeyValuePair<string, ICartItem> cartItem, IEnumerable<IProductOffer> offers);
    }
}