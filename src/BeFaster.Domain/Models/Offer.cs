
using BeFaster.Core.Data;
using BeFaster.Core.Models;
using BeFaster.Domain.Enums;
using System;
using System.Collections.Generic;

namespace BeFaster.Domain
{
    public abstract class Offer : IOffer
    {
        public Guid OfferId { get; set; }
        public IProduct Product { get; set; }
        public string OfferDSL { get; set; }
        public int Quantity { get; set; }

        public OfferType OfferType { get; set; }

        public Offer(Guid speciaOfferId,
                     IProduct product,
                     OfferType offerType,
                     string speciaOfferDsl)
        {
            OfferId = speciaOfferId;
            OfferType = offerType;
            Product = product;
            OfferDSL = speciaOfferDsl;
        }

        public abstract void Apply(KeyValuePair<string, ICartItem> cartItem);
    }
}
