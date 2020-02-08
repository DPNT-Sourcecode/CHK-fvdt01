namespace BeFaster.Core.Data
{
    public interface ISkuItem
    {
        string SKU { get; set; }
        int? Price { get; set; }
        ISpecialOffer SpecialOffer { get; set; }
    }
}