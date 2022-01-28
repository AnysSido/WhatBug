using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.PrioritySchemes.Queries.GetEditPriorityScheme;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PrioritySchemes.Queries.GetEditPriorityScheme
{
    [Collection("ValidatorCollection")]
    public class GetEditPrioritySchemeQueryValidatorTests
    {
        private readonly GetEditPrioritySchemeQueryValidator _sut;

        public GetEditPrioritySchemeQueryValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new GetEditPrioritySchemeQueryValidator(fixture.CreateContext());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void Given_ZeroOrLowerSchemeId_HasArgumentException(int id)
        {
            // Arrange
            var query = new GetEditPrioritySchemeQuery { Id = id };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.Id, typeof(ArgumentException));
        }

        [Fact]
        public async void Given_InvalidSchemeId_HasRecordNotFoundException()
        {
            // Arrange
            var query = new GetEditPrioritySchemeQuery { Id = 4 };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.Id, typeof(RecordNotFoundException));
        }
    }
}