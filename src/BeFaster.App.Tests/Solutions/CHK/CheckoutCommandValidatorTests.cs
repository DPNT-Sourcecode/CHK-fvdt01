using BeFaster.Core.Data;
using BeFaster.Data;
using BeFaster.Domain;
using BeFaster.Domain.Cqrs;
using BeFaster.Domain.Cqrs.Validators;
using FluentAssertions;
using NSubstitute;
using System.Linq;
using Xunit;

namespace BeFaster.App.Tests.Solutions.CHK
{
    public class CheckoutCommandValidatorTests
    {
        [Theory]
        [InlineData("ABC", true)]
        public void CheckoutCommandValidator_ReturnsNoErrors_WithValidSkus(string skus, bool expected)
        {
            //arrange
            var command = new CheckoutCommand {Skus= skus};
            var repository = Substitute.For<SkuRepositoryInMemory>();

            //act
            var validator = new CheckoutCommandValidator(repository);
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().Be(0);
            result.IsValid.Should().Equals(expected);
        }

        [Theory]
        [InlineData("ZZZZ", false)]
        public void CheckoutCommandValidator_ReturnsErrors_WithInValidSkus(string skus, bool expected)
        {
            //arrange
            var command = new CheckoutCommand { Skus = skus };
            var repository = Substitute.For<SkuRepositoryInMemory>();

            //act
            var validator = new CheckoutCommandValidator(repository);
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().Be(1);
            result.IsValid.Should().Equals(expected);
        }
    }
}
