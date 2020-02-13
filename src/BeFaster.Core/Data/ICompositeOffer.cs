using BeFaster.Core.Models;
using System.Collections.Generic;

namespace BeFaster.Core.Data
{
    public interface ICompositeOffer : IOffer
    {
        IProduct Product { get; set; }
        int? AtQuantity { get; set; }
        int? AtPrice { get; set; }
        int? FreeSkuQuantity { get; set; }
        IProduct FreeSku { get; set; }
        ICart Cart { get; set; }
        void Apply(KeyValuePair<string, ICartItem> cartItem);
    }
}