using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Admin.Commands.CreateRole;
using Xunit;

namespace WhatBug.Application.UnitTests.Admin.Commands.CreateRole
{
    public class CreateRoleValidatorTest
    {
        private CreateRoleCommandValidator _validator;

        public CreateRoleValidatorTest()
        {
            _validator = new CreateRoleCommandValidator();
        }

        [Fact]
        public void GivenEmptyName_ThrowsValidationException()
        {
            // Arrange
            var command = new CreateRoleCommand { Name = string.Empty };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name);
        }

        [Fact]
        public void GivenNullName_ThrowsValidationException()
        {
            // Arrange
            var command = new CreateRoleCommand ();

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name);
        }
    }
}
