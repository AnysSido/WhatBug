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

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GivenNullOrEmptyName_HasValidationError(string name)
        {
            // Arrange
            var command = new CreatePermissionSchemeCommand { Name = name };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage("Scheme name cannot be empty");
        }
    }
}
