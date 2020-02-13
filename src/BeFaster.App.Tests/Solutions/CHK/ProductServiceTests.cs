using BeFaster.Core.Data;
using BeFaster.Data;
using BeFaster.Domain.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace BeFaster.App.Tests.Solutions.CHK
{
    public class ProductServiceTests
    {
        [Fact]
        public void ProductServiceContructor_ThrowsArgumentException_WhenLoggerNull()
        {
            //arrange            
            var skuRepository = Substitute.For<IProductRepository>();
            
            //act
            Action action = () => new ProductService(null, skuRepository);
            
            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ProductServiceContructor_ThrowsArgumentException_WhenProductRepositoryNull()
        {
            //arrange            
            var logger = Substitute.For<ILogger<ProductService>>();
 
            //act
            Action action = () => new ProductService(logger, null);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async void ProductService_GetProducts_ReturnsResult()
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
    }
}
