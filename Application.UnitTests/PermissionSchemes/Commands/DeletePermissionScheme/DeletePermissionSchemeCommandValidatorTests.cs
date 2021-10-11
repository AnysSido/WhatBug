using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.PermissionSchemes.Commands.DeletePermissionScheme;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Commands.DeletePermissionScheme
{
    [Collection("ValidatorCollection")]
    public class DeletePermissionSchemeCommandValidatorTests
    {
        private DeletePermissionSchemeCommandValidator _sut;

        public DeletePermissionSchemeCommandValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new DeletePermissionSchemeCommandValidator(fixture.CreateContext());
        }

        [Fact]
        public void Given_ValidRequest_HasNoValidationErrors()
        {
            // Arrange
            var command = new DeletePermissionSchemeCommand { SchemeId = 2 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Given_ZeroOrLowerSchemeId_HasArgumentException(int id)
        {
            // Arrange
            var command = new DeletePermissionSchemeCommand { SchemeId = id };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.SchemeId, typeof(ArgumentException));
        }

        [Fact]
        public void Given_InvalidSchemeId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new DeletePermissionSchemeCommand { SchemeId = 4 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.SchemeId, typeof(RecordNotFoundException));
        }

        [Fact]
        public void Given_IdOfDefaultScheme_HasArgumentException()
        {
            // Arrange
            var command = new DeletePermissionSchemeCommand { SchemeId = 1 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.SchemeId, typeof(ArgumentException));
        }
    }
}