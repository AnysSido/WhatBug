using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.PrioritySchemes.Commands.EditPriorityScheme;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PrioritySchemes.Commands.EditPriorityScheme
{
    [Collection("ValidatorCollection")]
    public class EditPrioritySchemeCommandValidatorTests
    {
        private EditPrioritySchemeCommandValidator _sut;

        public EditPrioritySchemeCommandValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new EditPrioritySchemeCommandValidator(fixture.CreateContext());
        }

        [Fact]
        public void Given_ValidRequest_PassesValidation()
        {
            // Arrange
            var command = new EditPrioritySchemeCommand { Id = 1, Name = "NewName", Description = "NewDesc", PriorityIds = new[] { 1, 2 } };

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
            var command = new EditPrioritySchemeCommand { Id = id };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.Id, typeof(ArgumentException));
        }

        [Fact]
        public void Given_InvalidSchemeId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new EditPrioritySchemeCommand { Id = 4 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.Id, typeof(RecordNotFoundException));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GivenNullOrEmptyName_HasValidationError(string name)
        {
            // Arrange
            var command = new EditPrioritySchemeCommand { Name = name };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage("Priority scheme name cannot be empty");
        }

        [Fact]
        public void Given_DuplicateName_HasValidationError()
        {
            // Arrange
            var command = new EditPrioritySchemeCommand { Id = 3, Name = "PriorityScheme1" };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage("A priority scheme with the name PriorityScheme1 already exists");
        }

        [Theory]
        [InlineData(1, 2, 9)]
        [InlineData(4, 5, 7)]
        public void Given_AnyInvalidPriorityId_HasRecordNotFoundException(int priorityId1, int priorityId2, int priorityId3)
        {
            // Arrange
            var command = new EditPrioritySchemeCommand
            {
                Id = 1,
                PriorityIds = new int[] { priorityId1, priorityId2, priorityId3 }
            };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.PriorityIds, typeof(RecordNotFoundException));
        }
    }
}