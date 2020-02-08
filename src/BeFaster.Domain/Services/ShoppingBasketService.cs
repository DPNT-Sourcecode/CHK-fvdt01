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
        private readonly ISpecialOfferRepository _specialOfferRepository;

        public ShoppingBasketService(ILogger<ShoppingBasketService> logger,
                                    ISkuRepository skuRepository,
                                    ISpecialOfferRepository specialOfferRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _specialOfferRepository = specialOfferRepository ?? throw new ArgumentNullException(nameof(specialOfferRepository));
            _skuRepository = skuRepository ?? throw new ArgumentNullException(nameof(skuRepository));
        }

        public async Task<int> Checkout(string skus)
        {
            int total = 0;

            total = await CalculateTotal(skus);

            return total;
        }

        private async Task<int> CalculateTotal(string skus)
        {
            int total = 0;
            var skusRepository = await _skuRepository.GetAll();
            var skuAggregates = GetSkuAggregates(skus);
            skuAggregates.ToList().ForEach(async skuAggregate =>
            {
                var item = skusRepository.Where(x => x.SKU.ToUpper().Equals(skuAggregate.Sku.ToString().ToUpper())).FirstOrDefault();
                if (item != null)
                {
                    var subTotal = await CalculateItemTotal(skuAggregate, item);
                    total = total + subTotal;
                }
            });

            var freeItemsTotal = await CalculateFreeItemsTotal(skuAggregates);
            total = total - freeItemsTotal;

            return total;
        }
        private async Task<int> CalculateFreeItemsTotal(IEnumerable<dynamic> skuAggregates)
        {
            var freeItemsTotal = 0;
            var eItem = skuAggregates.Where(x => x.Sku.ToString().ToUpper().Equals("E")).FirstOrDefault();
            var bItem = skuAggregates.Where(x => x.Sku.ToString().ToUpper().Equals("B")).FirstOrDefault();

            var skus = await _skuRepository.GetAll();
            var sku = skus.Where(s => s.SKU.ToUpper().Equals("B")).FirstOrDefault();

            if (eItem?.Count >= 2 && bItem?.Count > 0)
            {
                var specialOffers = await _specialOfferRepository.GetAll();
                var specialOffer = specialOffers.Where(o => o.Sku.Equals("B")).FirstOrDefault();
                freeItemsTotal = specialOffer.Price;
                //var freeBItems = eItem.Count / 2;
                //freeItemsTotal = freeBItems * sku.Price.Value;
            }

            return freeItemsTotal;
        }

        public async Task<int> CalculateItemTotal(dynamic aggregateSku, ISkuItem item)
        {
            var offers = await _specialOfferRepository.GetAll();
            var specialOffers = offers.Where(s => s.Sku.Equals(item.SKU)).ToList().OrderByDescending(x=>x.Quantity);

            var skuItemCount = aggregateSku.Count;
            int offerSubTotal = 0;
            int standardSubTotal = 0;

            specialOffers.ToList().ForEach(specialOffer =>
            {
                if (skuItemCount >= specialOffer.Quantity)
                {
                    var quantityAtOfferPrice = skuItemCount / specialOffer.Quantity;
                    var quantityUnallocated = skuItemCount % specialOffer.Quantity;
                    offerSubTotal = offerSubTotal + (quantityAtOfferPrice * specialOffer.Price);
               
                    //adjust the running skuCount as offers are allocated
                    skuItemCount = quantityUnallocated;
                }                
            });
            
            //apply the standard price to the remaining skus
            standardSubTotal = skuItemCount > 0 ? skuItemCount * item.Price : 0;
            var total = offerSubTotal + standardSubTotal;
            return total;
        }

        public IEnumerable<dynamic> GetSkuAggregates(string skus)
        {
            return skus.GroupBy(c => c).Select(c => new { Sku = c.Key, Count = c.Count() });
        }
    }
}
