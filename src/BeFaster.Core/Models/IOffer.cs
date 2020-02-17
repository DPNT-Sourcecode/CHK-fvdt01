using System;

namespace BeFaster.Core.Models
{
    public interface IOffer
    {
        Guid? OfferId { get; set; }
        string OfferDSL { get; set; }        
    }
}