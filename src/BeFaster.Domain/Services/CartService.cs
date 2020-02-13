using BeFaster.Core.Builders;
using BeFaster.Core.Data;
using BeFaster.Core.Models;
using BeFaster.Core.Services;
using BeFaster.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BeFaster.Domain.Services
{
    public class CartService : ICartService
    {
        private readonly ILogger<CartService> _logger;
        private readonly IProductService _productService;
        private readonly IOfferService _offerService;
        private readonly ICartBuilder _cartBuilder;

        public CartService(ILogger<CartService> logger,
                           IProductService productService,
                           IOfferService offerService,
                           ICartBuilder cartBuilder)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _cartBuilder = cartBuilder ?? throw new ArgumentNullException(nameof(cartBuilder));

        }

        public async Task<int> Checkout(string skus)
        { 
            var cart = await _cartBuilder.Build(skus);
            var total = Calculate(cart);
            return total;
        }

        public int CalculateWithoutOffers(ICart cart)
        {
            int total = 0;
            cart.Items.OrderBy(x => x.Value.Product.Sku)
                .ToList()
                .ForEach(cartItem =>
                {
                    total = total + cartItem.Value.Product.Price.Value * cartItem.Value.Count;
                });
            
            return total;
        }

        public int Calculate(ICart cart)
        {
            cart.Items.OrderBy(x => x.Value.Product.Sku)
                .ToList()
                .ForEach(cartItem =>
                {                    
                    var skuOffers = _offerService.Lookup(cartItem.Value.Product.Sku);
                    if (skuOffers != null)
                    {
                        skuOffers.ToList().ForEach(offer =>
                        {
                            if (!cartItem.Value.Allocated.Value)
                            {
                                if (cartItem.Value.Count > 0)
                                {
                                    offer.Cart = cart;
                                    offer.Apply(cartItem);
                                }
                            }                            
                        });
                    }
                    else
                    {
                        var cartSummaryItem = new CartSummaryItem
                        {
                            Quantity = cartItem.Value.Count,
                            Price = cartItem.Value.Product.Price,
                            Total = cartItem.Value.Count * cartItem.Value.Product.Price
                        };
                        cart.Summary.Add(cartSummaryItem);                        
                    }
                });

            return cart.CalculateTotal();
        } 
    }
}
