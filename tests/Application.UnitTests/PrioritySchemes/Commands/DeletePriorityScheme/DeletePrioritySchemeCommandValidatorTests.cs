using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.PrioritySchemes.Commands.DeletePriorityScheme;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PrioritySchemes.Commands.DeletePriorityScheme
{
    [Collection("ValidatorCollection")]
    public class DeletePrioritySchemeCommandValidatorTests
    {
        private DeletePrioritySchemeCommandValidator _sut;

        public DeletePrioritySchemeCommandValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new DeletePrioritySchemeCommandValidator(fixture.CreateContext());
        }

        [Fact]
        public void Given_ValidRequest_HasNoValidationErrors()
        {
            // Arrange
            var command = new DeletePrioritySchemeCommand { SchemeId = 2 };

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
            var command = new DeletePrioritySchemeCommand { SchemeId = id };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.SchemeId, typeof(ArgumentException));
        }

        [Fact]
        public void Given_InvalidSchemeId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new DeletePrioritySchemeCommand { SchemeId = 4 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.SchemeId, typeof(RecordNotFoundException));
        }

        [Fact]
        public void Given_IdOfDefaultScheme_HasArgumentException()
        {
            // Arrange
            var command = new DeletePrioritySchemeCommand { SchemeId = 3 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.SchemeId, typeof(ArgumentException));
        }
    }
}