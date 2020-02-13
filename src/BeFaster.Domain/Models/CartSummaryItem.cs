

using BeFaster.Core.Data;
using BeFaster.Core.Models;

namespace BeFaster.Domain.Models
{
    public class CartSummaryItem : ICartSummaryItem
    {
        public IProduct Product { get; set; }
        public int Quantity { get; set; }
        public int? Price { get; set; }
        public int? Total { get; set; }
    }
}
