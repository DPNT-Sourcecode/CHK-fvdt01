using BeFaster.Core.Builders;
using BeFaster.Core.Data;
using BeFaster.Core.Services;
using BeFaster.Domain.Enums;
using BeFaster.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BeFaster.Domain.DSL
{
    public class OfferBuilder : IOfferBuilder
    {
        private IOfferRepository _offerRepository;
        private IProductService _productService;
        private Dictionary<string, IProduct> _productLookup;
        private Dictionary<string, int> _numberLookup;

        public OfferBuilder(IOfferRepository offerRepository,
                            IProductService productService)
        {
            _offerRepository = offerRepository ?? throw new ArgumentNullException(nameof(offerRepository));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));

            InitialiseProductLookup();
            InitialiseNumberLookup();
        }

        private async void InitialiseProductLookup()
        {
            var products = await _productService.GetProducts();

            _productLookup = new Dictionary<string, IProduct>();
            _productLookup = products.ToDictionary(k => k.Sku, v => v);
        }
        private void InitialiseNumberLookup()
        {
            _numberLookup = new Dictionary<string, int>();
            _numberLookup.Add("one", 1);
            _numberLookup.Add("two", 2);
            _numberLookup.Add("three", 3);
            _numberLookup.Add("four", 4);
            _numberLookup.Add("five", 5);
            _numberLookup.Add("six", 6);
            _numberLookup.Add("seven", 7);
            _numberLookup.Add("eight", 8);
            _numberLookup.Add("nine", 9);
            _numberLookup.Add("ten", 10);
        }

        public async Task<ICompositeOffer> Build(string offerDsl)
        {
            ICompositeOffer result = null;

            if (offerDsl.Contains("for"))
                result = await BuildBuyOffer(offerDsl);
            else
            {
                if (offerDsl.Contains("get"))
                    result = await BuildFreeOffer(offerDsl);
            }


            return result;
        }

        public async Task<ICompositeOffer> BuildBuyOffer(string offerDsl)
        {
            var items = Regex.Split(offerDsl, " ");

            var quantity = Convert.ToInt32(items[0].Substring(0, 1));
            var sku = items[0].Substring(1, 1);
            var product = _productLookup[sku];            
            var price = Convert.ToInt32(items[2]);            
            var offers = await _offerRepository.GetAll();
            var offer = offers.Where(x => x.OfferDSL.Equals(offerDsl)).FirstOrDefault();
            if (offer == null)
                return null;

            var dependency = offers.Where(x => x.OfferDSL.Equals(offerDsl)).FirstOrDefault();

            var result = new ForOffer(offer.OfferId,
                                      product,
                                      OfferType.BuyXForY,
                                      offer.OfferDSL,
                                      quantity,
                                      price);
            return result;
        }

        public async Task<ICompositeOffer> BuildFreeOffer(string offerDsl)
        {
            var items = Regex.Split(offerDsl, " ");

            var forQuantity = Convert.ToInt32(items[0].Substring(0, 1));
            var sku = items[0].Substring(1, 1);
            var product = _productLookup[sku];
            var freeQuantity = _numberLookup[items[2]];            
            var freeSku = _productLookup[items[3]];
            var offers = await _offerRepository.GetAll();
            var offer = offers.Where(x => x.OfferDSL.Equals(offerDsl)).FirstOrDefault();
            if (offer == null)
                return null;

            var result = new FreeOffer(offer.OfferId,
                                       product,
                                       null,
                                       OfferType.BuyXGetYFree,
                                       offer.OfferDSL,                                              
                                       forQuantity,
                                       freeQuantity,
                                       freeSku);
            return result;
        }

    }
}
