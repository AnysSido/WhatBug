using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.ProjectRoles.Queries.GetDeleteRoleConfirm;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.ProjectRoles.Queries.GetDeleteRoleConfirm
{
    [Collection("ValidatorCollection")]
    public class GetDeleteRoleConfirmQueryValidatorTests
    {
        private readonly GetDeleteRoleConfirmQueryValidator _sut;

        public GetDeleteRoleConfirmQueryValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new GetDeleteRoleConfirmQueryValidator(fixture.Context);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void Given_ZeroOrLowerRoleId_HasArgumentException(int id)
        {
            // Arrange
            var query = new GetDeleteRoleConfirmQuery { RoleId = id };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.RoleId, typeof(ArgumentException));
        }

        [Fact]
        public async void Given_InvalidRoleId_HasRecordNotFoundException()
        {
            // Arrange
            var query = new GetDeleteRoleConfirmQuery { RoleId = 4 };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.RoleId, typeof(RecordNotFoundException));
        }
    }
}