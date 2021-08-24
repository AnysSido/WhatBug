using FluentValidation.TestHelper;
using WhatBug.Application.PermissionSchemes.Commands.CreatePermissionScheme;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Commands.CreatePermissionScheme
{
    public class CreatePermissionSchemeValidatorTests
    {
        private CreatePermissionSchemeCommandValidator _validator;

        public CreatePermissionSchemeValidatorTests()
        {
            _validator = new CreatePermissionSchemeCommandValidator();
        }

        [Fact]
        public void GivenEmptyName_ThrowsValidationException()
        {
            // Arrange
            var command = new CreatePermissionSchemeCommand { Name = string.Empty };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name);
        }

        [Fact]
        public void GivenNullName_ThrowsValidationException()
        {
            // Arrange
            var command = new CreatePermissionSchemeCommand ();

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name);
        }
    }
}
