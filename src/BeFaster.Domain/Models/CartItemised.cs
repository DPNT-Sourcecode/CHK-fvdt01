using BeFaster.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.Domain.Models
{
    public class CartItemised : ICartItemised
    {
        public IList<ICartItemisedItem> Items { get; set; }

        public CartItemised(IList<ICartItemisedItem> offers)
        {
            Items = offers;
        }

        public void Add(ICartItemisedItem item)
        {
            Items.Add(item);
        }

        public int CalculateTotal()
        {
            int total = 0;

            total = Items.Where(i=> i.Free==false).Sum(i => i.Total).Value;
            return total;
        }
    }
}
