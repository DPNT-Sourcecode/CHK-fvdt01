using BeFaster.Core.Data;

namespace BeFaster.Data
{
    public class SkuItem : ISkuItem
    {
        public string SKU { get; set; }
        public ISpecialOffer SpecialOffer { get; set; }
        public int? Price { get; set; }
    }
}
