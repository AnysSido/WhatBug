using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Priorities.Commands.DeletePriority;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.Priorities.Commands.DeletePriority
{
    [Collection("ValidatorCollection")]
    public class DeletePriorityCommandValidatorTests
    {
        private DeletePriorityCommandValidator _sut;

        public DeletePriorityCommandValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new DeletePriorityCommandValidator(fixture.CreateContext());
        }

        [Fact]
        public void Given_ValidRequest_HasNoValidationErrors()
        {
            // Arrange
            var command = new DeletePriorityCommand { PriorityId = 2 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Given_ZeroOrLowerPrioirityId_HasArgumentException(int id)
        {
            // Arrange
            var command = new DeletePriorityCommand { PriorityId = id };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.PriorityId, typeof(ArgumentException));
        }

        [Fact]
        public void Given_InvalidPriorityId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new DeletePriorityCommand { PriorityId = 5 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.PriorityId, typeof(RecordNotFoundException));
        }

        [Fact]
        public void Given_IdOfDefaultPriority_HasArgumentException()
        {
            // Arrange
            var command = new DeletePriorityCommand { PriorityId = 4 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.PriorityId, typeof(ArgumentException));
        }
    }
}