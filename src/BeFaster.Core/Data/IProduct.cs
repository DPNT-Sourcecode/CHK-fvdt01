namespace BeFaster.Core.Data
{
    public interface IProduct
    {        
        string Sku { get; set; }
        int? Price { get; set; }
    }
}