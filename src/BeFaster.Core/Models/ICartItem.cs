using BeFaster.Core.Data;

namespace BeFaster.Core.Models
{
    public interface ICartItem
    {
        IProduct Product { get; set; }
        int Count { get; set; }
        int? Price { get; set; }
        int? Total { get; set; }
        bool? Allocated { get; set; }
    }
}