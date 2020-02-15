using BeFaster.Core.Models;
using System.Collections.Generic;

namespace BeFaster.Core.Models
{
    public interface IOfferSummary
    {
        IList<IOfferSummaryItem> Items { get; set; }
        int CalculateTotal();
        void Add(IOfferSummaryItem item);
    }
}