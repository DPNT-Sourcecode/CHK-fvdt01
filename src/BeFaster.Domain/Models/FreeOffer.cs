using BeFaster.Core.Enums;
using BeFaster.Core.Models;
using BeFaster.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.Domain
{
    public class FreeOffer : Offer, IProductOffer
    {
        public int? AtOfferQuantity { get; set; }
        public int? AtOfferPrice { get; set; }
        public int? FreeOfferQuantity { get; set; }
        public IProduct FreeOfferProduct { get; set; }
        public ICart Cart { get; set; }
        public FreeOffer(Guid? offerId,
                         IProduct product,
                         ICart cart,
                         OfferType offerType,
                         string dsl,      
                         int atOfferQuantity,
                         int freeOfferQuantity,
                         IProduct freeOfferProduct) : base(offerId, product, offerType, dsl)
        {
            Product = product;
            AtOfferQuantity = atOfferQuantity;
            FreeOfferQuantity = freeOfferQuantity;
            FreeOfferProduct = freeOfferProduct;
            Cart = cart;
        }

        public override void Apply(KeyValuePair<string, ICartItem> cartItem,
                                   IEnumerable<IProductOffer> offers)
        {
            KeyValuePair<string, ICartItem>? freeItem;
            freeItem = Cart.Items.Where(x => x.Value.Product.Sku.Equals(FreeOfferProduct.Sku)).FirstOrDefault();
            var hasFreeItem = string.IsNullOrEmpty(freeItem.Value.Key) && freeItem.Value.Value == null ? false : true;                              
            if (cartItem.Value.AvailableQuantity.Value >= AtOfferQuantity.Value && hasFreeItem)
            {                
                //get the target free item sku from the cart
                var freeSkuCartItem = this.Cart.Items[freeItem.Value.Value.Product.Sku];
                if (freeSkuCartItem != null)
                {
                    var sku = freeItem.Value.Value.Product.Sku;
                    int quantityNew = 0;
                    var removeItems = this.Cart.Itemised.Items.Where(x => x.Product.Sku.Equals(sku)).ToList();
                    if (removeItems.Any())
                    {
                        removeItems.ForEach(x => {
                            this.Cart.Itemised.Items.Remove(x);
                            if (quantityNew == 0)
                            {
                                quantityNew = x.AtQuantity.Value - this.FreeOfferQuantity.Value;
                            }                            
                        });

                        if (quantityNew > 0)
                        {
                            var test = new CartItemisedItem
                            {
                                AtPrice = freeSkuCartItem.Product.Price,
                                AtQuantity = quantityNew,
                                Total = freeSkuCartItem.Product.Price * quantityNew,
                                Product = freeSkuCartItem.Product,
                                Free = false
                            };
                            this.Cart.Itemised.Add(test);
                        }
                    }

                    var freeItemItemised = new CartItemisedItem
                    {
                        Product = freeSkuCartItem.Product,
                        Offer = this,
                        AtPrice = this.FreeOfferProduct.Price,
                        AtQuantity = this.FreeOfferQuantity,
                        Total = this.FreeOfferQuantity * this.FreeOfferProduct.Price,
                        Free = true
                    };                    
                    this.Cart.Itemised.Add(freeItemItemised);

                    var cartItemisedItem = new CartItemisedItem
                    {
                        AtPrice = cartItem.Value.Product.Price,
                        AtQuantity = AtOfferQuantity.Value,
                        Total = cartItem.Value.Product.Price * AtOfferQuantity.Value,
                        Product = cartItem.Value.Product,
                        Free = false
                    };
                    this.Cart.Itemised.Add(cartItemisedItem);

                    var remainingQuantity = cartItem.Value.AvailableQuantity.Value - AtOfferQuantity.Value;
                    cartItem.Value.AvailableQuantity = remainingQuantity;
                };             
            }
        }
    }
}
