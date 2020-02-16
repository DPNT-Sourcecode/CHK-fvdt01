using BeFaster.Core.Models;
using System.Collections.Generic;

namespace BeFaster.Core.Models
{
    public interface ICartItemised
    {
        IList<ICartItemisedItem> Items { get; set; }
        int CalculateTotal();
        void Add(ICartItemisedItem item);
    }
}