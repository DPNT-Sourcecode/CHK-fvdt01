

using BeFaster.Core.Data;
using BeFaster.Core.Models;
using System;

namespace BeFaster.Domain.Models
{
    public class CartSummaryItem : ICartSummaryItem
    {
        public IProduct Product { get; set; }
        public Guid? OfferId { get; set; }
        public int Quantity { get; set; }
        public int? Price { get; set; }
        public int? Total { get; set; }
    }
}
