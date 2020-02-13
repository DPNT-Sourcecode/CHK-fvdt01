using BeFaster.Core.Builders;
using BeFaster.Core.Data;
using BeFaster.Core.Models;
using BeFaster.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFaster.Domain.DSL
{
    public class CartBuilder : ICartBuilder
    {
        private readonly IProductRepository _productRepository;
        public CartBuilder(IProductRepository repository)
        {
            _productRepository = repository ?? throw new ArgumentNullException(nameof(repository));            
        }
        public async Task<ICart> Build(string skus)
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
                    Count = item.Count, 
                    Total = 0, 
                    Allocated = false
                };
                cartItems.Add(sku.Sku,cartItem);
            });

            return new Cart(cartItems, new CartSummary(new List<ICartSummaryItem>()));
        }
    }
}
