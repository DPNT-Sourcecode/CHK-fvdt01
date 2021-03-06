﻿using BeFaster.Domain;
using BeFaster.Domain.Cqrs;
using BeFaster.Domain.Cqrs.Validators;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace BeFaster.App.Tests.Solutions.SUM
{
    public class CalculateSumValidatorTests 
    {
        [Theory]
        [InlineData(1, 1,2)]
        public void CalculateSumCommandValidator_ReturnsNoErrors_WhenCommandValid(int? param1, int? param2, int expected)
        {
            //arrange
            var command = new CalculateSumCommand {Param1= param1, Param2=param2};

            //act
            var validator = new CalculateSumCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().Be(0);
            result.Should().Equals(expected);
        }

        [Theory]
        [InlineData(null,1)]        
        public void CalculateSumCommandValidator_ReturnsError_WhenParam1Empty(int? param1, int? param2)
        {
            //arrange
            var command = new CalculateSumCommand { Param1=param1, Param2= param2};

            //act
            var validator = new CalculateSumCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Equals(1);
            result.Errors.FirstOrDefault().ErrorCode
                                          .Should().Equals(ValidationErrors.Param1IsRequired.Code);
            result.Errors.FirstOrDefault().ErrorMessage
                                          .Should().Equals(ValidationErrors.Param1IsRequired.Message);
        }

        [Theory]
        [InlineData(1, null)]
        public void CalculateSumCommandValidator_ReturnsError_WhenParam2Empty(int? param1, int? param2)
        {
            //arrange
            var command = new CalculateSumCommand { Param1 = param1, Param2 = param2 };
            
            //act
            var validator = new CalculateSumCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Equals(1);
            result.Errors.FirstOrDefault().ErrorCode
                                          .Should().Equals(ValidationErrors.Param2IsRequired.Code);
            result.Errors.FirstOrDefault().ErrorMessage
                                          .Should().Equals(ValidationErrors.Param2IsRequired.Message);
        }

        [Theory]
        [InlineData(-1, 200)]
        public void CalculateSumCommandValidator_ReturnsError_WhenParam1LessThanZero(int? param1, int? param2)
        {
            //arrange
            var command = new CalculateSumCommand { Param1 = param1, Param2 = param2 };

            //act
            var validator = new CalculateSumCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Equals(1);
            result.Errors.FirstOrDefault().ErrorCode
                                          .Should().Equals(ValidationErrors.Param1IsRequired.Code);
            result.Errors.FirstOrDefault().ErrorMessage
                                          .Should().Equals(ValidationErrors.Param1IsRequired.Message);
        }

        [Theory]
        [InlineData(1, 200)]
        public void CalculateSumCommandValidator_ReturnsError_WhenParam2GreaterThan100(int? param1, int? param2)
        {
            //arrange
            var command = new CalculateSumCommand { Param1 = param1, Param2 = param2 };

            //act
            var validator = new CalculateSumCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Equals(1);
            result.Errors.FirstOrDefault().ErrorCode
                                          .Should().Equals(ValidationErrors.Param2IsInvalid.Code);
            result.Errors.FirstOrDefault().ErrorMessage
                                          .Should().Equals(ValidationErrors.Param2IsInvalid.Message);
        }
    }
}
