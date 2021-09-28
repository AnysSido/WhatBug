using Application.UnitTests.Common;
using FluentValidation.TestHelper;
using WhatBug.Application.ProjectRoles.Commands.CreateRole;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.Admin.Commands.CreateRole
{
    public class CreateRoleValidatorTest : CommandTestBase
    {
        private CreateRoleCommandValidator _validator;

        public CreateRoleValidatorTest()
        {
            _validator = new CreateRoleCommandValidator(_context);
        }

        [Fact]
        public void GivenEmptyName_HasValidationError()
        {
            // Arrange
            var command = new CreateRoleCommand { Name = string.Empty };
            var expectedErrorMessage = "Role name cannot be empty";

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage(expectedErrorMessage);
        }

        [Fact]
        public void GivenNullName_HasValidationError()
        {
            // Arrange
            var command = new CreateRoleCommand ();
            var expectedErrorMessage = "Role name cannot be empty";

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage(expectedErrorMessage);
        }

        [Fact]
        public void Given_DuplicateName_HasValidationError()
        {
            // Arrange
            using (var context = CreateContext())
            {
                context.Roles.Add(new Role { Id = 1, Name = "Developer" });
                context.SaveChanges();
            }
            var command = new CreateRoleCommand { Name = "Developer" };
            var expectedErrorMessage = "A role with the name Developer already exists";

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage(expectedErrorMessage);
        }
    }
}
