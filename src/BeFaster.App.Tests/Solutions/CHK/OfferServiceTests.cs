using BeFaster.Core.Data;
using BeFaster.Data;
using BeFaster.Domain.DSL;
using BeFaster.Domain.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Linq;
using Xunit;

namespace BeFaster.App.Tests.Solutions.CHK
{
    public class OfferServiceTests
    {
        [Fact]
        public void OfferServiceContructor_ThrowsArgumentException_WhenLoggerNull()
        {        
            //arrange     
            var offerRepository = Substitute.For<OfferRepositoryInMemory>();
            var productLogger = Substitute.For<ILogger<ProductService>>();
            var productRepository = Substitute.For<ProductRepositoryInMemory>();
            var productService = new ProductService(productLogger, productRepository);                                
            var offerBuilder = new OfferFactory(offerRepository, productService);

            //act
            Action action = () => new OfferService(null, productRepository, offerRepository, offerBuilder);
            
            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void OfferServiceContructor_ThrowsArgumentException_WhenOfferRepositoryNull()
        {
            //arrange     
            var offerRepository = Substitute.For<OfferRepositoryInMemory>();
            var productRepository = Substitute.For<ProductRepositoryInMemory>();
            var productServiceLogger = Substitute.For<ILogger<ProductService>>();
            var offerServiceLogger = Substitute.For<ILogger<OfferService>>();
            var productService = new ProductService(productServiceLogger, productRepository);
            var offerBuilder = new OfferFactory(offerRepository, productService);

            //act
            Action action = () => new OfferService(offerServiceLogger, productRepository, null, offerBuilder);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void OfferServiceContructor_ThrowsArgumentException_WhenProductRepositoryNull()
        {       
            //arrange     
            var offerRepository = Substitute.For<OfferRepositoryInMemory>();
            var productRepository = Substitute.For<ProductRepositoryInMemory>();
            var productServiceLogger = Substitute.For<ILogger<ProductService>>();
            var offerServiceLogger = Substitute.For<ILogger<OfferService>>();
            var productService = new ProductService(productServiceLogger, productRepository);
            var offerBuilder = new OfferFactory(offerRepository, productService);

            //act
            Action action = () => new OfferService(offerServiceLogger, null, offerRepository, offerBuilder);


            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void OfferServiceContructor_ThrowsArgumentException_WhenOfferFactoryNull()
        {
            //arrange     
            var offerRepository = Substitute.For<OfferRepositoryInMemory>();
            var productRepository = Substitute.For<ProductRepositoryInMemory>();
            var productServiceLogger = Substitute.For<ILogger<ProductService>>();
            var offerServiceLogger = Substitute.For<ILogger<OfferService>>();
            var productService = new ProductService(productServiceLogger, productRepository);
            var offerBuilder = new OfferFactory(offerRepository, productService);


            //act
            Action action = () => new OfferService(offerServiceLogger, productRepository,offerRepository, null);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async void OfferService_GetProducts_ReturnsResult()
        {
            //arrange
            var logger = Substitute.For<ILogger<ProductService>>();
            var skuRepository = Substitute.For<ProductRepositoryInMemory>();

            //act
            var service = new ProductService(logger, skuRepository);
            var result = await service.GetProducts();

            //assert
            result.Should().HaveCount(5);
        }

        [Theory]
        [InlineData("A", 2)]
        public void OfferService_Lookup_ReturnsResults(string sku, int expected)
        {
            //arrange     
            var offerRepository = Substitute.For<OfferRepositoryInMemory>();
            var productRepository = Substitute.For<ProductRepositoryInMemory>();
            var productServiceLogger = Substitute.For<ILogger<ProductService>>();
            var offerServiceLogger = Substitute.For<ILogger<OfferService>>();
            var productService = new ProductService(productServiceLogger, productRepository);
            var offerBuilder = new OfferFactory(offerRepository, productService);
            var service = new OfferService(offerServiceLogger, productRepository,offerRepository, offerBuilder);

            var result = service.Lookup(sku);

            //assert
            result.Should().NotBeNull();
            result.Count().Should().Be(expected);
        }

        [Fact]
        public async void OfferService_GetOffers_ReturnsResult()
        {
            //arrange     
            var offerRepository = Substitute.For<OfferRepositoryInMemory>();
            var productRepository = Substitute.For<ProductRepositoryInMemory>();
            var productServiceLogger = Substitute.For<ILogger<ProductService>>();
            var offerServiceLogger = Substitute.For<ILogger<OfferService>>();
            var productService = new ProductService(productServiceLogger, productRepository);
            var offerBuilder = new OfferFactory(offerRepository, productService);
            var service = new OfferService(offerServiceLogger, productRepository, offerRepository, offerBuilder);
            
            //fact
            var result = await service.GetOffers();

            //assert
            result.Count().Should().BeGreaterThan(0);
        }
    }
}
