using BeFaster.Core.Enums;
using BeFaster.Core.Models;
using System;
using System.Collections.Generic;

namespace BeFaster.Domain
{
    public abstract class Offer : IOffer
    {
        public Guid? OfferId { get; set; }
        public IProduct Product { get; set; }
        public string OfferDSL { get; set; }
        public int Quantity { get; set; }
        public OfferType OfferType { get; set; }        

        public Offer(Guid? offerId,
                     IProduct product,
                     OfferType offerType,
                     string speciaOfferDsl)
        {
            OfferId = offerId;
            OfferType = offerType;
            Product = product;
            OfferDSL = speciaOfferDsl;
        }

        public abstract void Apply(KeyValuePair<string, ICartItem> cartItem,
                                   IEnumerable<IProductOffer> offers);
    }
}
