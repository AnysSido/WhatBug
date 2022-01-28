using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.PermissionSchemes.Commands.GrantRolePermissions;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Commands.GrantRolePermissions
{
    [Collection("ValidatorCollection")]
    public class GrantRolePermissionsCommandValidatorTests
    {
        private GrantRolePermissionsCommandValidator _sut;

        public GrantRolePermissionsCommandValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new GrantRolePermissionsCommandValidator(fixture.CreateContext());
        }

        [Fact]
        public void Given_ValidRequest_PassesValidation()
        {
            // Arrange
            var command = new GrantRolePermissionsCommand { SchemeId = 1, RoleId = 1, PermissionIds = new[] { 4, 5 } };

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
            var command = new GrantRolePermissionsCommand { SchemeId = id };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.SchemeId, typeof(ArgumentException));
        }

        [Fact]
        public void Given_InvalidSchemeId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new GrantRolePermissionsCommand { SchemeId = 4 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.SchemeId, typeof(RecordNotFoundException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Given_ZeroOrLowerRoleId_HasArgumentException(int id)
        {
            // Arrange
            var command = new GrantRolePermissionsCommand { RoleId = id };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.RoleId, typeof(ArgumentException));
        }

        [Fact]
        public void Given_InvalidRoleId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new GrantRolePermissionsCommand { RoleId = 4 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.RoleId, typeof(RecordNotFoundException));
        }

        [Fact]
        public void Given_NullIds_HasArgumentException()
        {
            // Arrange
            var command = new GrantRolePermissionsCommand { SchemeId = 1, RoleId = 1 };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.PermissionIds, typeof(ArgumentException));
        }

        [Theory]
        [InlineData(4, 5, 9)]
        [InlineData(10, 11, 12)]
        public void Given_AnyInvalidPermissionId_HasRecordNotFoundException(int permissionId1, int permissionId2, int permissionId3)
        {
            // Arrange
            var command = new GrantRolePermissionsCommand
            {
                SchemeId = 1,
                RoleId = 1,
                PermissionIds = new int[] { permissionId1, permissionId2, permissionId3 }
            };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.PermissionIds, typeof(RecordNotFoundException));
        }

        [Fact]
        public void Given_PermissionOfWrongType_HasArgumentException()
        {
            // Arrange
            var command = new GrantRolePermissionsCommand
            {
                SchemeId = 1,
                RoleId = 1,
                PermissionIds = new int[] { 1 }
            };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.PermissionIds, typeof(ArgumentException));
        }
    }
}