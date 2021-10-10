using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.PermissionSchemes.Commands.EditPermissionScheme;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Commands.EditPermissionScheme
{
    [Collection("ValidatorCollection")]
    public class EditPermissionSchemeCommandValidatorTests
    {
        private EditPermissionSchemeCommandValidator _sut;

        public EditPermissionSchemeCommandValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new EditPermissionSchemeCommandValidator(fixture.CreateContext());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GivenNullOrEmptyName_HasValidationError(string name)
        {
            // Arrange
            var command = new EditPermissionSchemeCommand{ Name = name };

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
            var command = new EditPermissionSchemeCommand { SchemeId = 3, Name = "PermissionScheme2" };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage("A scheme with the name PermissionScheme2 already exists");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Given_ZeroOrLowerSchemeId_HasArgumentException(int id)
        {
            // Arrange
            var command = new EditPermissionSchemeCommand { SchemeId = id };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.SchemeId, typeof(ArgumentException));
        }

        [Fact]
        public void Given_InvalidSchemeId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new EditPermissionSchemeCommand { SchemeId = 4 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.SchemeId, typeof(RecordNotFoundException));
        }
    }
}
