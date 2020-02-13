using BeFaster.Core.Data;
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
    public class CartBuilderTests
    {
        [Fact]
        public void CartBuilderContructor_ThrowsArgumentException_WhenProductRepositoryNull()
        {
            //arrange            
            var skuRepository = Substitute.For<IProductRepository>();

            //act
            Action action = () => new CartBuilder(null);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]        
        [InlineData("ABCD")]
        public async void CartBuilder_Build_ReturnsResult(string skus)
        {
            //arrange
            var logger = Substitute.For<ILogger<ProductService>>();
            var productRepository = Substitute.For<ProductRepositoryInMemory>();
            
            //act
            var builder = new CartBuilder(productRepository);
            var result = await builder.Build(skus);

            //assert
            result.Items.Should().HaveCount(4);
        }

    }
}
