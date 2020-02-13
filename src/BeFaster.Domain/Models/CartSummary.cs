

using BeFaster.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.Domain.Models
{
    public class CartSummary : ICartSummary
    {
        public IList<ICartSummaryItem> Items { get; set; }

        public CartSummary(IList<ICartSummaryItem> items)
        {
            Items = items;
        }

        public void Add(ICartSummaryItem item)
        {
            Items.Add(item);
        }
        public int Calculate()
        {
            int total = 0;
            total= Items.Sum(i => i.Total).Value;
            return total;
        }
    }
}
