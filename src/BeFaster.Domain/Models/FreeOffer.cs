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
                    var removeItems = this.Cart.Offers.Items.Where(x => x.Product.Sku.Equals(sku)).ToList();
                    if (removeItems.Any())
                    {
                        removeItems.ForEach(x => {
                            this.Cart.Offers.Items.Remove(x);
                        });
                    }
                    
                    var offerSummaryItem = new OfferSummaryItem
                    {
                        Product = freeSkuCartItem.Product,
                        Offer = this,
                        AtPrice = this.FreeOfferProduct.Price,
                        AtQuantity = this.FreeOfferQuantity,
                        Total = this.FreeOfferQuantity * this.FreeOfferProduct.Price
                    };                    
                    this.Cart.Offers.Add(offerSummaryItem);
                };

                //var standardItem = new CartSummaryItem
                //{
                //    Product = this.Product,
                //    OfferId = null,
                //    Quantity = AtOfferQuantity.Value,
                //    Price = this.Product.Price.Value,
                //    Total = this.Product.Price.Value * AtOfferQuantity.Value
                //};
                //this.Cart.Summary.Add(standardItem);

                //cartItem.Value.AvailableQuantity = cartItem.Value.AvailableQuantity.Value - AtOfferQuantity.Value;
            }
        }
    }
}
