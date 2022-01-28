using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.UnitTests.Common;
using WhatBug.Application.UserPermissions.Queries.GetGrantUserPermissions;
using Xunit;

namespace WhatBug.Application.UnitTests.UserPermissions.Queries.GetGrantUserPermissions
{
    [Collection("ValidatorCollection")]
    public class GetGrantUserPermissionsQueryValidatorTests
    {
        private readonly GetGrantUserPermissionsQueryValidator _sut;

        public GetGrantUserPermissionsQueryValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new GetGrantUserPermissionsQueryValidator(fixture.CreateContext());
        }

        [Fact]
        public async void Given_ValidRequest_PassesValidation()
        {
            // Arrange
            var query = new GetGrantUserPermissionsQuery { UserId = 2 };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void Given_ZeroOrLowerUserId_HasArgumentException(int id)
        {
            // Arrange
            var query = new GetGrantUserPermissionsQuery { UserId = id };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.UserId, typeof(ArgumentException));
        }

        [Fact]
        public async void Given_InvalidUserId_HasRecordNotFoundException()
        {
            // Arrange
            var query = new GetGrantUserPermissionsQuery { UserId = 4 };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.UserId, typeof(RecordNotFoundException));
        }
    }
}