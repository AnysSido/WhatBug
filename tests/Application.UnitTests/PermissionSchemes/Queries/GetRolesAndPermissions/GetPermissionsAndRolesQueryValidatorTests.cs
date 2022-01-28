using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.PermissionSchemes.Queries.GetRolesAndPermissions;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Queries.GetPermissionsAndRoles
{
    [Collection("ValidatorCollection")]
    public class GetPermissionsAndRolesQueryValidatorTests
    {
        private readonly GetRolesAndPermissionsQueryValidator _sut;

        public GetPermissionsAndRolesQueryValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new GetRolesAndPermissionsQueryValidator(fixture.CreateContext());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void Given_ZeroOrLowerSchemeId_HasArgumentException(int id)
        {
            // Arrange
            var query = new GetRolesAndPermissionsQuery { SchemeId = id };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.SchemeId, typeof(ArgumentException));
        }

        [Fact]
        public async void Given_InvalidSchemeId_HasRecordNotFoundException()
        {
            // Arrange
            var query = new GetRolesAndPermissionsQuery { SchemeId = 4 };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.SchemeId, typeof(RecordNotFoundException));
        }
    }
}