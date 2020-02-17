using System.Collections.Generic;

namespace BeFaster.Core.Models
{
    public interface ICartSummary
    {
        IList<ICartSummaryItem> Items { get; set; }
        int Calculate(); 
        void Add(ICartSummaryItem item);
    }
}