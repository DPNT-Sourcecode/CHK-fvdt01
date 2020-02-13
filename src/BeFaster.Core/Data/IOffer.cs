using System;

namespace BeFaster.Core.Data
{
    public interface IOffer
    {
        Guid OfferId { get; set; }
        string OfferDSL { get; set; }
    }
}