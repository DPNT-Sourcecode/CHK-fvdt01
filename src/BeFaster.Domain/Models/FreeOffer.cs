
using BeFaster.Core.Data;
using BeFaster.Core.Models;
using BeFaster.Domain.Enums;
using BeFaster.Domain.Models;
using System;
using System.Collections.Generic;

namespace BeFaster.Domain
{
    public class FreeOffer : Offer, ICompositeOffer
    {
        public int? AtQuantity { get; set; }
        public int? AtPrice { get; set; }
        public int? FreeSkuQuantity { get; set; }
        public IProduct FreeSku { get; set; }
        public ICart Cart { get; set; }
        
        public FreeOffer(Guid speciaOfferId,
                         IProduct product,
                         ICart cart,
                         OfferType offerType,
                         string dsl,                                
                         int atQuantity,
                         int freeQuantity,
                         IProduct freeSku) : base(speciaOfferId,product, offerType, dsl)
        {
            AtQuantity = atQuantity;
            FreeSkuQuantity = freeQuantity;
            FreeSku = freeSku;
            Cart = cart;
        }


        public override void Apply(KeyValuePair<string, ICartItem> cartItem)
        {            
            if (cartItem.Value.Count >= AtQuantity.Value)
            {
                //get the target free item sku from the cart
                var freeSkuCartItem= this.Cart.Items[this.FreeSku.Sku];
                if (freeSkuCartItem != null)
                {
                    var quantityForFree = freeSkuCartItem.Count > 0 ? this.FreeSkuQuantity:0;

                    var freeSummaryItem = new CartSummaryItem
                    {
                        Product = freeSkuCartItem.Product,
                        Quantity = quantityForFree.Value,
                        Price = -freeSkuCartItem.Product.Price,
                        Total = quantityForFree.Value * -freeSkuCartItem.Product.Price
                    };
                    this.Cart.Summary.Add(freeSummaryItem);
                }


                var summaryItem = new CartSummaryItem
                {
                    Quantity = cartItem.Value.Count,
                    Price = this.Product.Price.Value,
                    Total = cartItem.Value.Count * this.Product.Price.Value
                };
                this.Cart.Summary.Add(summaryItem);
                cartItem.Value.Allocated = true;
            }
            else
            {
                var summaryItem = new CartSummaryItem
                {
                    Quantity = cartItem.Value.Count,
                    Price = this.Product.Price.Value,
                    Total = cartItem.Value.Count * this.Product.Price.Value
                };
                this.Cart.Summary.Add(summaryItem);
                cartItem.Value.Allocated = true;
            }
        }
    }
}
