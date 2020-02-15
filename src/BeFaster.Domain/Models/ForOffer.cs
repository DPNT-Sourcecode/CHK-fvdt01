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
            if (cartItem.Value.AvailableQuantity > 0)
            {
                if (cartItem.Value.AvailableQuantity >= AtOfferQuantity.Value)
                {
                    var item = this.Cart.Items.ToList().Where(x => x.Value.Product.Sku.Equals(this.Product.Sku)).SingleOrDefault();
                    var itemTotal = this.Product.Price * item.Value.Quantity;
                    var offerSummaryItem = new OfferSummaryItem
                    {
                        Offer = this,
                        AtPrice = this.AtOfferPrice.Value,
                        AtQuantity = this.AtOfferQuantity.Value,
                        Total = itemTotal - this.AtOfferPrice.Value,
                        Product = this.Product
                    };
                    this.Cart.Offers.Add(offerSummaryItem);
                    var remainingQuantity = cartItem.Value.AvailableQuantity.Value - AtOfferQuantity.Value;
                    cartItem.Value.AvailableQuantity = remainingQuantity;
                    
                    //var item = this.Cart.Items.ToList().Where(x => x.Value.Product.Sku.Equals(this.Product.Sku)).SingleOrDefault();
                    //var itemTotal = this.Product.Price * item.Value.Quantity;
                    //var remainingQuantity = cartItem.Value.AvailableQuantity.Value - AtOfferQuantity.Value;
                    //var remainingTotal = this.Product.Price.Value * remainingQuantity;
                    //var offerSummaryItem = new OfferSummaryItem
                    //{
                    //    Offer = this,
                    //    AtPrice = this.AtOfferPrice.Value,
                    //    AtQuantity = this.AtOfferQuantity.Value,
                    //    Total = itemTotal-this.AtOfferPrice.Value- remainingTotal,
                    //    Product = this.Product
                    //};
                    this.Cart.Offers.Add(offerSummaryItem);
                    cartItem.Value.AvailableQuantity = remainingQuantity;
                    
                    //var remainingQuantity = cartItem.Value.AvailableQuantity.Value - AtOfferQuantity.Value;
                    //cartItem.Value.AvailableQuantity = remainingQuantity;

                    //if (remainingQuantity > 0)
                    //{
                    //    var standardItem = new OfferSummaryItem
                    //    {
                    //        Offer = this,
                    //        AtPrice = this.AtOfferPrice.Value,
                    //        AtQuantity = this.AtOfferQuantity.Value,
                    //        Total = itemTotal - (this.Product.Price.Value * remainingQuantity),
                    //        Product = this.Product
                    //    };
                    //    this.Cart.Offers.Add(offerSummaryItem);
                    //    cartItem.Value.AvailableQuantity = cartItem.Value.AvailableQuantity.Value - remainingQuantity;
                    //    cartItem.Value.Processed = true;
                    //}
                }
            }
            

        }
    }
}
