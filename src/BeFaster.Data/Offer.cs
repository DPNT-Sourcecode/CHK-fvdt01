using BeFaster.Core.Models;
using System;

namespace BeFaster.Data
{
    public class Offer : IOffer
    {
        public Guid? OfferId { get; set; }
        public string OfferDSL { get; set; }   
        public int? Priority { get; set; }
    }
}