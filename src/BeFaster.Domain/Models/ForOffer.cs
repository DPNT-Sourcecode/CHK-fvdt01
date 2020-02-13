using BeFaster.Core.Data;
using BeFaster.Core.Models;
using BeFaster.Domain.Enums;
using BeFaster.Domain.Models;
using System;
using System.Collections.Generic;

namespace BeFaster.Domain
{
    public class ForOffer : Offer, ICompositeOffer
    {        
        public int? AtQuantity { get; set; }
        public int? AtPrice { get; set; }
        public int? FreeSkuQuantity { get; set; }
        public IProduct FreeSku { get; set; }
        public ICart Cart { get; set; }       
        public ForOffer(Guid speciaOfferId,
                        IProduct product,
                        OfferType offerType,
                        string dsl,                                           
                        int atQuantity, 
                        int atPrice) : base(speciaOfferId, product, offerType, dsl)
        {
            AtQuantity = atQuantity;
            AtPrice = atPrice;            
        }

        public override void Apply(KeyValuePair<string,ICartItem> cartItem)
        {
            if (cartItem.Value.Count > AtQuantity.Value)
            {
                var offerSummaryItem = new CartSummaryItem
                {
                    Product = cartItem.Value.Product,
                    Quantity = AtQuantity.Value,
                    Price = this.AtPrice.Value,
                    Total = this.AtPrice.Value
                };
                this.Cart.Summary.Add(offerSummaryItem);

                var standardItem = new CartSummaryItem
                {
                    Quantity = cartItem.Value.Count - AtQuantity.Value,
                    Price = this.Product.Price.Value,
                    Total = this.Product.Price.Value * (cartItem.Value.Count - AtQuantity.Value)
                };
                this.Cart.Summary.Add(standardItem);

                cartItem.Value.Allocated = true;
            }
            else
            {
                var standardItem = new CartSummaryItem
                {
                    Quantity = cartItem.Value.Count,
                    Price = this.Product.Price.Value,
                    Total = cartItem.Value.Count * this.Product.Price.Value
                };
                this.Cart.Summary.Add(standardItem);
                cartItem.Value.Allocated = true;
            }
        }
    }
}