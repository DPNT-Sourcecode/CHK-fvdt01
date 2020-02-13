using BeFaster.Core.Data;

namespace BeFaster.Core.Models
{
    public interface ICartSummaryItem
    {
        int? Price { get; set; }
        IProduct Product { get; set; }
        int Quantity { get; set; }
        int? Total { get; set; }
    }
}