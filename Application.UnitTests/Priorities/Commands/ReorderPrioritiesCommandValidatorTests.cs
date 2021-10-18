using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using WhatBug.Application.Priorities.Commands.ReorderPriorities;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.Priorities.Commands
{
    [Collection("ValidatorCollection")]
    public class ReorderPrioritiesCommandValidatorTests
    {
        private ReorderPrioritiesCommandValidator _sut;

        public ReorderPrioritiesCommandValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new ReorderPrioritiesCommandValidator(fixture.CreateContext());
        }

        [Fact]
        public void Given_ValidRequest_PassesValidation()
        {
            // Arrange
            var command = new ReorderPrioritiesCommand { Ids = new List<int> { 4, 3, 2, 1 } };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Given_NullIds_HasArgumentException()
        {
            // Arrange
            var command = new ReorderPrioritiesCommand { };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.Ids, typeof(ArgumentException));
        }

        [Fact]
        public void Given_EmptyIds_HasArgumentException()
        {
            // Arrange
            var command = new ReorderPrioritiesCommand { Ids = new List<int>() };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.Ids, typeof(ArgumentException));
        }

        [Theory]
        [InlineData(1, 2, 2, 3)]
        [InlineData(2, 3, 4, 9)]
        public void Given_DuplicateOrInvalidId_HasArgumentException(int id1, int id2, int id3, int id4)
        {
            // Arrange
            var command = new ReorderPrioritiesCommand { Ids = new List<int> { id1, id2, id3, id4 } };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.Ids, typeof(ArgumentException));
        }
    }
}