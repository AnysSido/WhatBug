using FluentValidation.TestHelper;
using WhatBug.Application.PermissionSchemes.Commands.CreatePermissionScheme;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Commands.CreatePermissionScheme
{
    [Collection("ValidatorCollection")]
    public class CreatePermissionSchemeValidatorTests
    {
        private CreatePermissionSchemeCommandValidator _sut;

        public CreatePermissionSchemeValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new CreatePermissionSchemeCommandValidator(fixture.CreateContext());
        }

        [Fact]
        public void Given_ValidRequest_HasNoValidationErrors()
        {
            // Arrange
            var command = new CreatePermissionSchemeCommand { Name = "New Scheme Name", Description = "New Scheme Desc" };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Given_NullOrEmptyName_HasValidationError(string name)
        {
            // Arrange
            var command = new CreatePermissionSchemeCommand { Name = name };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage("Scheme name cannot be empty");
        }

        [Fact]
        public void Given_DuplicateName_HasValidationError()
        {
            // Arrange
            var command = new CreatePermissionSchemeCommand { Name = "PermissionScheme1" };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage("A permission scheme with the name PermissionScheme1 already exists");
        }
    }
}
