using BeFaster.Core.Data;
using BeFaster.Core.Models;

namespace BeFaster.Data
{
    public class Product : IProduct
    {
        public string Sku { get; set; }
        public int? Price { get; set; }
    }
}
