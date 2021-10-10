using Application.UnitTests.Common;
using FluentValidation.TestHelper;
using WhatBug.Application.ProjectRoles.Commands.CreateRole;
using WhatBug.Application.UnitTests.Common;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.ProjectRoles.Commands.CreateRole
{
    [Collection("ValidatorCollection")]
    public class CreateRoleValidatorTest
    {
        private CreateRoleCommandValidator _sut;

        public CreateRoleValidatorTest(ValidatorTestFixture fixture)
        {
            _sut = new CreateRoleCommandValidator(fixture.CreateContext());
        }

        [Fact]
        public void Given_ValidRequest_HasNoValidationErrors()
        {
            // Arrange
            var command = new CreateRoleCommand { Name = "New Role Name", Description = "New Role Desc" };

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
            var command = new CreateRoleCommand { Name = name };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage("Role name cannot be empty");
        }

        [Fact]
        public void Given_DuplicateName_HasValidationError()
        {
            // Arrange
            var command = new CreateRoleCommand { Name = "Developer" };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage("A role with the name Developer already exists");
        }
    }
}
