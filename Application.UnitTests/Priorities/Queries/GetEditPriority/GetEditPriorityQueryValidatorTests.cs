using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Priorities.Queries.GetEditPriority;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.Priorities.Queries.GetEditPriority
{
    [Collection("ValidatorCollection")]
    public class GetEditPriorityQueryValidatorTests
    {
        private readonly GetEditPriorityQueryValidator _sut;

        public GetEditPriorityQueryValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new GetEditPriorityQueryValidator(fixture.CreateContext());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void Given_ZeroOrLowerPriorityId_HasArgumentException(int id)
        {
            // Arrange
            var query = new GetEditPriorityQuery { Id = id };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.Id, typeof(ArgumentException));
        }

        [Fact]
        public async void Given_InvalidPriorityId_HasRecordNotFoundException()
        {
            // Arrange
            var query = new GetEditPriorityQuery { Id = 5 };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.Id, typeof(RecordNotFoundException));
        }
    }
}
