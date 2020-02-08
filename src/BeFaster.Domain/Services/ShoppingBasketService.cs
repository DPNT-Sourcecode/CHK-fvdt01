using BeFaster.Core.Data;
using BeFaster.Core.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFaster.Domain.Services
{
    public class ShoppingBasketService : IShoppingBasketService
    {
        private readonly ILogger<ShoppingBasketService> _logger;
        private readonly ISkuRepository _skuRepository;
        
        public ShoppingBasketService(ILogger<ShoppingBasketService> logger,
                                    ISkuRepository skuRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _skuRepository = skuRepository ?? throw new ArgumentNullException(nameof(skuRepository));
        }

        public async Task<int> Checkout(string skus)
        {
            int total = 0;

            var skusRepository = await _skuRepository.GetAll();
            var skuAggregates = GetSkuAggregates(skus);
            skuAggregates.ToList().ForEach(skuAggregate =>
            {
                var item = skusRepository.Where(x => x.SKU.ToUpper().Equals(skuAggregate.Sku.ToString().ToUpper())).FirstOrDefault();
                if (item != null)
                {
                    var subTotal = CalculateItemTotal(skuAggregate, item);
                    total = total + subTotal;
                }
            });

            return total;
        }

        public int CalculateItemTotal(dynamic aggregateSku, ISkuItem sku)
        {
            var total = sku.Price * aggregateSku.Count;
            if (sku.SpecialOffer != null)
            {
                total = sku.SpecialOffer.Quantity == aggregateSku.Count ?
                        sku.SpecialOffer.Price :
                        total;
            }

            return total;
        }

        public IEnumerable<dynamic> GetSkuAggregates(string skus)
        {
            return skus.GroupBy(c => c).Select(c => new { Sku = c.Key, Count = c.Count() });
        }
    }
}
