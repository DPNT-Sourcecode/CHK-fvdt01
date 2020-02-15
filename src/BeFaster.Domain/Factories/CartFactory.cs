using BeFaster.Core.Factories;
using BeFaster.Core.Data;
using BeFaster.Core.Models;
using BeFaster.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFaster.Domain.DSL
{
    public class CartFactory : ICartFactory
    {
        private readonly IProductRepository _productRepository;
        public CartFactory(IProductRepository repository)
        {
            _productRepository = repository ?? throw new ArgumentNullException(nameof(repository));            
        }
        public async Task<ICart> Create(string skus)
        {
            var skusItems = await _productRepository.GetAll();
            var skulookUp = skusItems.ToDictionary(x => x.Sku, x => x);

            var cartItems = new Dictionary<string,ICartItem>();
            var results =  skus.GroupBy(c => c).Select(c => new { Sku = c.Key, Count = c.Count() });
            results.ToList().ForEach(item =>
            {
                var sku= skulookUp[item.Sku.ToString()];
                var cartItem = new CartItem 
                { 
                    Product = sku,
                    Offer = null,
                    Quantity = item.Count,
                    AvailableQuantity = item.Count,
                    Total = 0, 
                    Processed = false
                };
                cartItems.Add(sku.Sku,cartItem);
            });

            return new Cart(cartItems,
                   new CartSummary(new List<ICartSummaryItem>()),
                   new OfferSummary(new List<IOfferSummaryItem>()));
        }
    }
}
