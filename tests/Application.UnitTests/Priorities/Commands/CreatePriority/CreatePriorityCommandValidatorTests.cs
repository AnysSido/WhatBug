using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Priorities.Commands.CreatePriority;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.Priorities.Commands.CreatePriority
{
    [Collection("ValidatorCollection")]
    public class CreatePriorityCommandValidatorTests
    {
        private CreatePriorityCommandValidator _sut;

        public CreatePriorityCommandValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new CreatePriorityCommandValidator(fixture.CreateContext());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GivenNullOrEmptyName_HasValidationError(string name)
        {
            // Arrange
            var command = new CreatePriorityCommand { Name = name };

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
            var command = new CreatePriorityCommand { Name = "Priority1" };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage("A priority with the name Priority1 already exists");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Given_ZeroOrLowerColorId_HasArgumentException(int id)
        {
            // Arrange
            var command = new CreatePriorityCommand { ColorId = id };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.ColorId, typeof(ArgumentException));
        }

        [Fact]
        public void Given_InvalidColorId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new CreatePriorityCommand { ColorId = 4 };

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
            var command = new CreatePriorityCommand { IconId = id };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.IconId, typeof(ArgumentException));
        }

        [Fact]
        public void Given_InvalidIconId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new CreatePriorityCommand { IconId = 4 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.IconId, typeof(RecordNotFoundException));
        }
    }
}
