using Application.UnitTests.Common;
using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.ProjectRoles.Commands.EditRole;
using WhatBug.Application.UnitTests.Common;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.ProjectRoles.Commands.EditRole
{
    [Collection("ValidatorCollection")]
    public class EditRoleCommandValidatorTests
    {
        private EditRoleCommandValidator _sut;

        public EditRoleCommandValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new EditRoleCommandValidator(fixture.Context);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GivenNullOrEmptyName_HasValidationError(string name)
        {
            // Arrange
            var command = new EditRoleCommand { Name = name };
            var expectedErrorMessage = "Role name cannot be empty";

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage(expectedErrorMessage);
        }

        [Fact]
        public void Given_DuplicateName_HasValidationError()
        {
            // Arrange
            var command = new EditRoleCommand { RoleId = 4, Name = "Developer" };
            var expectedErrorMessage = "A role with the name Developer already exists";

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage(expectedErrorMessage);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Given_ZeroOrLowerRoleId_HasArgumentException(int id)
        {
            // Arrange
            var command = new EditRoleCommand { RoleId = id };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.RoleId, typeof(ArgumentException));
        }

        [Fact]
        public void Given_InvalidRoleId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new EditRoleCommand { RoleId = 4 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.RoleId, typeof(RecordNotFoundException));
        }
    }
}