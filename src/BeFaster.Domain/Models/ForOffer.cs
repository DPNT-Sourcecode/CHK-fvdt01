using BeFaster.Core.Enums;
using BeFaster.Core.Models;
using BeFaster.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.Domain
{
    public class ForOffer : Offer, IProductOffer
    {        
        public int? AtOfferQuantity { get; set; }
        public int? AtOfferPrice { get; set; }
        public int? FreeOfferQuantity { get; set; }
        public IProduct FreeOfferProduct { get; set; }
        public ICart Cart { get; set; }       
        public ForOffer(Guid offerId,
                        IProduct product,
                        ICart cart,
                        OfferType offerType,
                        string dsl,                                           
                        int atQuantity, 
                        int atPrice) : base(offerId, product, offerType, dsl)
        {
            AtOfferQuantity = atQuantity;
            AtOfferPrice = atPrice;
            Cart = cart;
            Product = product;
        }

        public override void Apply(KeyValuePair<string, ICartItem> cartItem,
                                   IEnumerable<IProductOffer> offers)
        {
            while (cartItem.Value.AvailableQuantity >= AtOfferQuantity.Value)
            {
                var cartItemisedItem = new CartItemisedItem
                {
                    Offer = this,
                    AtPrice = this.AtOfferPrice.Value,
                    AtQuantity = this.AtOfferQuantity.Value,
                    Total = this.AtOfferPrice.Value,
                    Product = this.Product,
                    Free = false
                };
                this.Cart.Itemised.Add(cartItemisedItem);
                var remainingQuantity = cartItem.Value.AvailableQuantity.Value - AtOfferQuantity.Value;
                cartItem.Value.AvailableQuantity = remainingQuantity;
            }
        }
    }
}