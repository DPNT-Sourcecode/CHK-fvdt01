using System;

namespace BeFaster.Core.Data
{
    public class Offer : IOffer
    {
        public Guid Id { get; set; }
        public string OfferDSL { get; set; }
        public Guid OfferId { get; set; }

    }
}