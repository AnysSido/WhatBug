using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.ProjectRoles.Commands.DeleteRole;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.ProjectRoles.Commands.DeleteRole
{
    [Collection("ValidatorCollection")]
    public class DeleteRoleCommandValidatorTests
    {
        private DeleteRoleCommandValidator _sut;

        public DeleteRoleCommandValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new DeleteRoleCommandValidator(fixture.Context);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Given_ZeroOrLowerRoleId_HasArgumentException(int id)
        {
            // Arrange
            var command = new DeleteRoleCommand { RoleId = id };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.RoleId, typeof(ArgumentException));
        }

        [Fact]
        public void Given_InvalidRoleId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new DeleteRoleCommand { RoleId = 4 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.RoleId, typeof(RecordNotFoundException));
        }
    }
}