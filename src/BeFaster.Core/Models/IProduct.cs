namespace BeFaster.Core.Models
{
    public interface IProduct
    {        
        string Sku { get; set; }
        int? Price { get; set; }
    }
}