using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.UnitTests.Common;
using WhatBug.Application.UserPermissions.Commands.GrantGlobalPermissions;
using WhatBug.Application.UserPermissions.Commands.GrantUserPermissions;
using Xunit;

namespace WhatBug.Application.UnitTests.UserPermissions.Commands.GrantUserPermissions
{
    [Collection("ValidatorCollection")]
    public class GrantUserPermissionsCommandValidatorTests
    {
        private GrantUserPermissionsCommandValidator _sut;

        public GrantUserPermissionsCommandValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new GrantUserPermissionsCommandValidator(fixture.CreateContext());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Given_ZeroOrLowerUserId_HasArgumentException(int id)
        {
            // Arrange
            var command = new GrantUserPermissionsCommand { UserId = id };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.UserId, typeof(ArgumentException));
        }

        [Fact]
        public void Given_InvalidUserId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new GrantUserPermissionsCommand { UserId = 4 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.UserId, typeof(RecordNotFoundException));
        }

        [Fact]
        public void Given_NullIds_HasArgumentException()
        {
            // Arrange
            var command = new GrantUserPermissionsCommand { UserId = 3 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.PermissionIds, typeof(ArgumentException));
        }

        [Theory]
        [InlineData(1, 2, 9)]
        [InlineData(4, 5, 6)]
        public void Given_AnyInvalidPermissionId_HasRecordNotFoundException(int permissionId1, int permissionId2, int permissionId3)
        {
            // Arrange
            var command = new GrantUserPermissionsCommand 
            {
                UserId = 1, PermissionIds = new int[] { permissionId1, permissionId2, permissionId3  } 
            };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.PermissionIds, typeof(RecordNotFoundException));
        }
    }
}