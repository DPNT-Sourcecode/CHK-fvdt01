using BeFaster.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.Domain.Models
{
    public class Cart : ICart
    {
        public IDictionary<string,ICartItem> Items { get; set; }
        public ICartSummary Summary { get; set; }
        public Cart(IDictionary<string,ICartItem> items,
                    ICartSummary summary)
        {
            Items = items;
            Summary = summary;
        }

        public int CalculateTotal()
        {
            int total = 0;
            this.Summary.Items.ToList().ForEach(item =>
            {
                total = total + item.Total.Value;
            });

            return total;
        }
    }
}
