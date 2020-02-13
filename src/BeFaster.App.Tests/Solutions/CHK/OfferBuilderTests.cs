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
    public class OfferBuilderTests
    {
        [Fact]
        public void OfferBuilderContructor_ThrowsArgumentException_WhenProductServiceNull()
        {
            //arrange     
            var offerRepository = Substitute.For<OfferRepositoryInMemory>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productRepository = Substitute.For<ProductRepositoryInMemory>();            

            //act
            Action action = () => new OfferBuilder(offerRepository, null);
            
            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void OfferBuilderContructor_ThrowsArgumentException_WhenOfferRepositoryNull()
        {
            //arrange     
            var offerRepository = Substitute.For<OfferRepositoryInMemory>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productRepository = Substitute.For<ProductRepositoryInMemory>();
            var productService = new ProductService(logger, productRepository);

            //act
            Action action = () => new OfferBuilder(null, productService);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("2B for 45")]
        public async void OfferBuilder_Build_ReturnsResult(string dsl)
        {
            //arrange     
            var offerRepository = Substitute.For<OfferRepositoryInMemory>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productRepository = Substitute.For<ProductRepositoryInMemory>();
            var productService = new ProductService(logger,productRepository);
           
            //act            
            var builder = new OfferBuilder(offerRepository, productService);
            var result = await builder.Build(dsl);

            //assert
            result.Should().NotBeNull();            
            result.AtQuantity.Should().Be(2);
            result.AtPrice.Should().Be(45);
            result.Product.Sku.Should().Be("B");
            
        }
    }
}
