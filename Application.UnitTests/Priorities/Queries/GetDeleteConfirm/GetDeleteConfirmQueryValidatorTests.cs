using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Priorities.Queries.GetDeleteConfirm;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.Priorities.Queries.GetDeleteConfirm
{
    [Collection("ValidatorCollection")]
    public class GetDeleteConfirmQueryValidatorTests
    {
        private readonly GetDeleteConfirmQueryValidator _sut;

        public GetDeleteConfirmQueryValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new GetDeleteConfirmQueryValidator(fixture.CreateContext());
        }

        [Fact]
        public async void Given_ValidId_HasNoValidationErrors()
        {
            // Arrange
            var query = new GetDeleteConfirmQuery { PriorityId = 2 };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void Given_ZeroOrLowerPriorityId_HasArgumentException(int id)
        {
            // Arrange
            var query = new GetDeleteConfirmQuery { PriorityId = id };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.PriorityId, typeof(ArgumentException));
        }

        [Fact]
        public async void Given_InvalidPriorityId_HasRecordNotFoundException()
        {
            // Arrange
            var query = new GetDeleteConfirmQuery { PriorityId = 5 };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.PriorityId, typeof(RecordNotFoundException));
        }
    }
}