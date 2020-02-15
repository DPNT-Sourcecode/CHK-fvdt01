using System.Collections.Generic;

namespace BeFaster.Core.Models
{
    public interface ICart
    {
        IDictionary<string,ICartItem> Items { get; set; }
        ICartSummary Summary { get; set; }
        IOfferSummary Offers { get; set; }
        int CalculateTotal();
    }
}