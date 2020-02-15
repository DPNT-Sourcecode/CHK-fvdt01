using BeFaster.Data;
using BeFaster.Domain.DSL;
using BeFaster.Domain.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using Xunit;

namespace BeFaster.App.Tests.Solutions.CHK
{
    public class OfferFactoryTests
    {
        [Fact]
        public void OfferFactoryContructor_ThrowsArgumentException_WhenProductServiceNull()
        {
            //arrange     
            var offerRepository = Substitute.For<OfferRepositoryInMemory>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productRepository = Substitute.For<ProductRepositoryInMemory>();            

            //act
            Action action = () => new OfferFactory(offerRepository, null);
            
            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void OfferFactoryContructor_ThrowsArgumentException_WhenOfferRepositoryNull()
        {
            //arrange     
            var offerRepository = Substitute.For<OfferRepositoryInMemory>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productRepository = Substitute.For<ProductRepositoryInMemory>();
            var productService = new ProductService(logger, productRepository);

            //act
            Action action = () => new OfferFactory(null, productService);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("2B for 45")]
        public async void OfferFactory_Build_ReturnsResult(string dsl)
        {
            //arrange     
            var offerRepository = Substitute.For<OfferRepositoryInMemory>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productRepository = Substitute.For<ProductRepositoryInMemory>();
            var productService = new ProductService(logger,productRepository);
           
            //act            
            var factory = new OfferFactory(offerRepository, productService);
            var result = await factory.Create(dsl);

            //assert
            result.Should().NotBeNull();            
            result.AtOfferQuantity.Should().Be(2);
            result.AtOfferPrice.Should().Be(45);
            result.Product.Sku.Should().Be("B");
            
        }
    }
}
