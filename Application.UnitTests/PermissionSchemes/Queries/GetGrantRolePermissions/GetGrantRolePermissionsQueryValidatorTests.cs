using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.PermissionSchemes.Queries.GetGrantRolePermissions;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Queries.GetGrantRolePermissions
{
    [Collection("ValidatorCollection")]
    public class GetGrantRolePermissionsQueryValidatorTests
    {
        private readonly GetGrantRolePermissionsQueryValidator _sut;

        public GetGrantRolePermissionsQueryValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new GetGrantRolePermissionsQueryValidator(fixture.CreateContext());
        }

        [Fact]
        public async void Given_ValidRequest_PassesValidation()
        {
            // Arrange
            var query = new GetGrantRolePermissionsQuery { SchemeId = 1, RoleId = 1 };

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
            var query = new GetGrantRolePermissionsQuery { SchemeId = id };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.SchemeId, typeof(ArgumentException));
        }

        [Fact]
        public async void Given_InvalidSchemeId_HasRecordNotFoundException()
        {
            // Arrange
            var query = new GetGrantRolePermissionsQuery { SchemeId = 4 };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.SchemeId, typeof(RecordNotFoundException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void Given_ZeroOrLowerRoleId_HasArgumentException(int id)
        {
            // Arrange
            var query = new GetGrantRolePermissionsQuery { RoleId = id };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.RoleId, typeof(ArgumentException));
        }

        [Fact]
        public async void Given_InvalidRoleId_HasRecordNotFoundException()
        {
            // Arrange
            var query = new GetGrantRolePermissionsQuery { RoleId = 4 };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.RoleId, typeof(RecordNotFoundException));
        }
    }
}