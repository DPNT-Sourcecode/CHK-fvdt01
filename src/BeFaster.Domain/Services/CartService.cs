using BeFaster.Core.Factories;
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
        private readonly ICartFactory _cartBuilder;

        public CartService(ILogger<CartService> logger,
                           IProductService productService,
                           IOfferService offerService,
                           ICartFactory cartBuilder)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _cartBuilder = cartBuilder ?? throw new ArgumentNullException(nameof(cartBuilder));

        }

        public async Task<int> Checkout(string skus)
        { 
            var cart = await _cartBuilder.Create(skus);
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
                    total = total + cartItem.Value.Product.Price.Value * cartItem.Value.Quantity.Value;
                });
            
            return total;
        }

        public int Calculate(ICart cart)
        {
            cart.Items.OrderBy(x => x.Value.Product.Sku)
                .ToList()
                .ForEach(cartItem =>
                {
                    var cartSummaryItem = new CartSummaryItem
                    {
                        Quantity = cartItem.Value.Quantity.Value,
                        OfferId = null,
                        Product = cartItem.Value.Product,
                        Price = cartItem.Value.Product.Price,
                        Total = cartItem.Value.Quantity.Value * cartItem.Value.Product.Price
                    };
                    cart.Summary.Add(cartSummaryItem);                    
                });

            cart.Items.OrderBy(x => x.Value.Product.Sku)
                .ToList()
                .ForEach(cartItem =>
                {
                    _offerService.ApplyOffers(cart, cartItem);
                });

            var totalOffers = cart.Offers.CalculateTotal();
            var totalCart = cart.CalculateTotal();
            var total = totalCart - totalOffers;

            return total;
        } 
    }
}
