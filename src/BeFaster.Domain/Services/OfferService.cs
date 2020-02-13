using BeFaster.Core.Builders;
using BeFaster.Core.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFaster.Domain.Services
{
    public class OfferService : IOfferService
    {
        private IOfferRepository _offerRepository;
        private IProductRepository _productRepository;
        private IOfferBuilder _builder;
        private ILogger<OfferService> _logger;
        private Dictionary<string, List<ICompositeOffer>> _offerLookup;


        public OfferService(ILogger<OfferService> logger,
                            IProductRepository productRepository,
                            IOfferRepository offerRepository,
                            IOfferBuilder builder)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _offerRepository = offerRepository ?? throw new ArgumentNullException(nameof(offerRepository));
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            InitLookup();
        }

        public async void InitLookup()
        {
            if (_offerLookup == null)
            {
                _offerLookup = new Dictionary<string, List<ICompositeOffer>>();
                var products = await _productRepository.GetAll();
                products.ForEach(async p =>
                {
                    var offers = new List<ICompositeOffer>();
                    var productOffers = await GetOffersBySku(p.Sku);
                    if (productOffers.Any())
                    {                        
                        productOffers.ToList().ForEach(async o =>
                        {
                            var offer = await _builder.Build(o.OfferDSL);
                            offers.Add(offer);
                        });
                    }
                    if (offers.Any())
                    {
                        _offerLookup.Add(p.Sku, offers);
                    }
                });
                
            }
        }

        public IEnumerable<ICompositeOffer> Lookup(string sku)
        {
            var offers = new List<ICompositeOffer>();
            _offerLookup.TryGetValue(sku, out offers);            
            return offers;
        }

        public async Task<IEnumerable<ICompositeOffer>> GetOffers()
        {
            var speciaOffers = new List<ICompositeOffer>();

            var offers = await _offerRepository.GetAll();
            offers.ForEach(async o =>
            {
                var offer=await _builder.Build(o.OfferDSL);
                speciaOffers.Add(offer);
            });

            return speciaOffers;
        }

        private async Task<IEnumerable<ICompositeOffer>> GetOffersBySku(string sku)
        {
            var results = await GetOffers();
            var offers = results.Where(x => x.Product.Sku.Equals(sku)).ToList().OrderByDescending(x => x.AtQuantity);            
            return offers;
        } 
    }
}
