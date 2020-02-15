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
    public class CartFactoryTests
    {
        [Fact]
        public void CartFactoryContructor_ThrowsArgumentException_WhenProductRepositoryNull()
        {
            //arrange            
            var skuRepository = Substitute.For<IProductRepository>();

            //act
            Action action = () => new CartFactory(null);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]        
        [InlineData("ABCD")]
        public async void CartFactory_Build_ReturnsResult(string skus)
        {
            //arrange
            var logger = Substitute.For<ILogger<ProductService>>();
            var productRepository = Substitute.For<ProductRepositoryInMemory>();
            
            //act
            var factory = new CartFactory(productRepository);
            var result = await factory.Create(skus);

            //assert
            result.Items.Should().HaveCount(4);
        }

    }
}
