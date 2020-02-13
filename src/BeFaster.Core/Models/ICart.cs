using System.Collections.Generic;

namespace BeFaster.Core.Models
{
    public interface ICart
    {
        IDictionary<string,ICartItem> Items { get; set; }
        ICartSummary Summary { get; set; }
        int CalculateTotal();
    }
}