using BeFaster.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.Domain.Models
{
    public class OfferSummary : IOfferSummary
    {
        public IList<IOfferSummaryItem> Items { get; set; }

        public OfferSummary(IList<IOfferSummaryItem> offers)
        {
            Items = offers;
        }

        public void Add(IOfferSummaryItem item)
        {
            Items.Add(item);
        }

        public int CalculateTotal()
        {
            int total = 0;

            total = Items.Sum(i => i.Total).Value;
            return total;
        }
    }
}
