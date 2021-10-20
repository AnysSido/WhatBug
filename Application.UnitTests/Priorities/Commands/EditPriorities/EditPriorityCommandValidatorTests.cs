using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Priorities.Commands.EditPriority;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.Priorities.Commands.EditPriorities
{
    [Collection("ValidatorCollection")]
    public class EditPriorityCommandValidatorTests
    {
        private EditPriorityCommandValidator _sut;

        public EditPriorityCommandValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new EditPriorityCommandValidator(fixture.CreateContext());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GivenNullOrEmptyName_HasValidationError(string name)
        {
            // Arrange
            var command = new EditPriorityCommand { Name = name };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage("Priority name cannot be empty");
        }

        [Fact]
        public void Given_DuplicateName_HasValidationError()
        {
            // Arrange
            var command = new EditPriorityCommand { Id = 1, Name = "Priority2" };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage("A priority with the name Priority2 already exists");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Given_ZeroOrLowerPriorityId_HasArgumentException(int id)
        {
            // Arrange
            var command = new EditPriorityCommand { Id = id };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.Id, typeof(ArgumentException));
        }

        [Fact]
        public void Given_InvalidPriorityId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new EditPriorityCommand { Id = 5 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.Id, typeof(RecordNotFoundException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Given_ZeroOrLowerColorId_HasArgumentException(int id)
        {
            // Arrange
            var command = new EditPriorityCommand { ColorId = id };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.ColorId, typeof(ArgumentException));
        }

        [Fact]
        public void Given_InvalidColorId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new EditPriorityCommand { ColorId = 4 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.ColorId, typeof(RecordNotFoundException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Given_ZeroOrLowerIconId_HasArgumentException(int id)
        {
            // Arrange
            var command = new EditPriorityCommand { IconId = id };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.IconId, typeof(ArgumentException));
        }

        [Fact]
        public void Given_InvalidIconId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new EditPriorityCommand { IconId = 4 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.IconId, typeof(RecordNotFoundException));
        }
    }
}