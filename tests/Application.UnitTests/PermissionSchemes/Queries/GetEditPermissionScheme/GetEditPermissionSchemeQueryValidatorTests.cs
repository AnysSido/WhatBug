using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.PermissionSchemes.Queries.GetEditPermissionScheme;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Queries.GetEditPermissionScheme
{
    [Collection("ValidatorCollection")]
    public class GetEditPermissionSchemeQueryValidatorTests
    {
        private readonly GetEditPermissionSchemeQueryValidator _sut;

        public GetEditPermissionSchemeQueryValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new GetEditPermissionSchemeQueryValidator(fixture.CreateContext());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void Given_ZeroOrLowerSchemeId_HasArgumentException(int id)
        {
            // Arrange
            var query = new GetEditPermissionSchemeQuery { SchemeId = id };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.SchemeId, typeof(ArgumentException));
        }

        [Fact]
        public async void Given_InvalidSchemeId_HasRecordNotFoundException()
        {
            // Arrange
            var query = new GetEditPermissionSchemeQuery { SchemeId = 4 };

            // Act
            var result = await _sut.TestValidateAsync(query);

            // Assert
            result.ShouldHaveExceptionFor(query => query.SchemeId, typeof(RecordNotFoundException));
        }
    }
}
