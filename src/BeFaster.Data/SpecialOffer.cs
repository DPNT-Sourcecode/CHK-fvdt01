namespace BeFaster.Core.Data
{
    public class SpecialOffer : ISpecialOffer
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}