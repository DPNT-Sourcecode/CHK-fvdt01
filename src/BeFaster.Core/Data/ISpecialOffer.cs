namespace BeFaster.Core.Data
{
    public interface ISpecialOffer
    {
        string Sku { get; set; }
        int Quantity { get; set; }
        int Price { get; set; }
    }
}