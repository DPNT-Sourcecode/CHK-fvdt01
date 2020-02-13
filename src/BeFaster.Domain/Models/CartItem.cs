

using BeFaster.Core.Data;
using BeFaster.Core.Models;

namespace BeFaster.Domain.Models
{
    public class CartItem : ICartItem
    {
        public IProduct Product { get; set; }
        public int Count { get; set; }
        public int? Total { get; set; }
        public int? Price { get; set; }
        public bool? Allocated { get; set; }
    }
}
