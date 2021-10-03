using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.ProjectRoles.Queries.GetEditRole;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.ProjectRoles.Queries.GetEditRole
{
    [Collection("ValidatorCollection")]
    public class GetEditRoleQueryValidatorTests
    {
        private readonly GetEditRoleQueryValidator _sut;

        public GetEditRoleQueryValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new GetEditRoleQueryValidator(fixture.Context);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void Given_ZeroOrLowerRoleId_HasArgumentException(int id)
        {
            // Arrange
            var query = new GetEditRoleQuery { RoleId = id };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.RoleId, typeof(ArgumentException));
        }

        [Fact]
        public async void Given_InvalidRoleId_HasRecordNotFoundException()
        {
            // Arrange
            var query = new GetEditRoleQuery { RoleId = 4 };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.RoleId, typeof(RecordNotFoundException));
        }
    }
}