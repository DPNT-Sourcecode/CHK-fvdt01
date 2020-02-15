using BeFaster.Core.Data;
using BeFaster.Core.Factories;
using BeFaster.Core.Models;
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
        private IOfferFactory _factory;
        private ILogger<OfferService> _logger;
        private Dictionary<string, List<IProductOffer>> _offerLookup;


        public OfferService(ILogger<OfferService> logger,
                            IProductRepository productRepository,
                            IOfferRepository offerRepository,
                            IOfferFactory factory)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _offerRepository = offerRepository ?? throw new ArgumentNullException(nameof(offerRepository));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            InitLookup();
        }

        public async void InitLookup()
        {
            if (_offerLookup == null)
            {
                _offerLookup = new Dictionary<string, List<IProductOffer>>();
                var products = await _productRepository.GetAll();
                products.ForEach(async p =>
                {
                    var offers = new List<IProductOffer>();
                    var productOffers = await GetOffersBySku(p.Sku);
                    if (productOffers.Any())
                    {                        
                        productOffers.ToList().ForEach(async o =>
                        {
                            var offer = await _factory.Create(o.OfferDSL);                            
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

        public IEnumerable<IProductOffer> Lookup(string sku)
        {
            var offers = new List<IProductOffer>();
            _offerLookup.TryGetValue(sku, out offers);            
            return offers;
        }

        public void ApplyOffers(ICart cart, 
                                KeyValuePair<string, ICartItem> cartItem)
        {
            var offers = Lookup(cartItem.Value.Product.Sku);
            if (offers != null)
            {                
                
                offers.ToList().ForEach(offer =>
                {
                    if (!cartItem.Value.Processed.Value)
                    {
                        offer.Cart = cart;
                        offer.Apply(cartItem, offers);
                    }
                });

                //apply remaining items at product price
                if (cartItem.Value.AvailableQuantity > 0)
                {
                    var standardItem = new OfferSummaryItem
                    {
                        Offer = this,
                        AtPrice = this.AtOfferPrice.Value,
                        AtQuantity = this.AtOfferQuantity.Value,
                        Total = itemTotal - (this.Product.Price.Value * remainingQuantity),
                        Product = this.Product
                    };
                    this.Cart.Offers.Add(offerSummaryItem);
                    cartItem.Value.AvailableQuantity = cartItem.Value.AvailableQuantity.Value - remainingQuantity;
                    cartItem.Value.Processed = true;                    
                }
            }
        }

        public async Task<IEnumerable<IProductOffer>> GetOffers()
        {
            var offers = new List<IProductOffer>();

            var items = await _offerRepository.GetAll();
            items.ForEach(async o =>
            {
                var offer=await _factory.Create(o.OfferDSL);
                offers.Add(offer);
            });

            return offers;
        }

        private async Task<IEnumerable<IProductOffer>> GetOffersBySku(string sku)
        {
            var results = await GetOffers();
            var offers = results.Where(x => x.Product.Sku.Equals(sku))
                                .ToList()
                                //.OrderByDescending(x => x.Priority);  
                                .OrderByDescending(x => x.AtOfferQuantity);            
            return offers;
        } 
    }
}

