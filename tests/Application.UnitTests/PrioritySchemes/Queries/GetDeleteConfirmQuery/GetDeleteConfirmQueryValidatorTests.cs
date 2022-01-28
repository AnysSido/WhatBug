using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.PrioritySchemes.Queries.GetDeleteConfirm;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PrioritySchemes.Queries.GetDeleteConfirm
{
    [Collection("ValidatorCollection")]
    public class GetDeleteConfirmQueryValidatorTests
    {
        private GetDeleteConfirmQueryValidator _sut;

        public GetDeleteConfirmQueryValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new GetDeleteConfirmQueryValidator(fixture.CreateContext());
        }

        [Fact]
        public async void Given_ValidId_HasNoValidationErrors()
        {
            // Arrange
            var query = new GetDeleteConfirmQuery { SchemeId = 2 };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void Given_ZeroOrLowerSchemeId_HasArgumentException(int id)
        {
            // Arrange
            var query = new GetDeleteConfirmQuery { SchemeId = id };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.SchemeId, typeof(ArgumentException));
        }

        [Fact]
        public async void Given_InvalidSchemeId_HasRecordNotFoundException()
        {
            // Arrange
            var query = new GetDeleteConfirmQuery { SchemeId = 4 };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.SchemeId, typeof(RecordNotFoundException));
        }
    }
}