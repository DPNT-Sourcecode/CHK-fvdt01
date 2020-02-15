using BeFaster.Core.Data;
using BeFaster.Core.Models;
using BeFaster.Core.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFaster.Domain.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _repository;
        private ILogger<ProductService> _logger;
        private Dictionary<string, IProduct> _productLookup;

        public ProductService(ILogger<ProductService> logger,
                              IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            InitLookup();
        }

        public async void InitLookup()
        {
            if (_productLookup == null)
            {
                _productLookup = new Dictionary<string, IProduct>();
                var products = await _repository.GetAll();
                products.ForEach(p =>
                {
                    _productLookup.Add(p.Sku, p);
                });
            }
        }

        public IProduct Lookup(string sku)
        {
            IProduct product;
            _productLookup.TryGetValue(sku, out product);
            return product;
        }

        public async Task<IEnumerable<IProduct>> GetProducts()
        {
            var skus = await _repository.GetAll();
            return skus;
        }
    }
}
