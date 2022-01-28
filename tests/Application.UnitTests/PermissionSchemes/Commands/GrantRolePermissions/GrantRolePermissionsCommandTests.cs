using Application.UnitTests.Common;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.PermissionSchemes.Commands.GrantRolePermissions;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Commands.GrantRolePermissions
{
    public class GrantRolePermissionsCommandTests : CommandTestBase
    {
        public GrantRolePermissionsCommandTests()
        {
            using (var context = CreateContext())
            {
                context.PermissionSchemes.Add(new PermissionScheme { Id = 1 });

                context.Roles.Add(new Role { Id = 1 });

                context.Permissions.AddRange(new Permission[]
                {
                    new Permission { Id = 1, Name = "Permission1", Description = "Permission1", Type = PermissionType.Project },
                    new Permission { Id = 2, Name = "Permission2", Description = "Permission2", Type = PermissionType.Project },
                    new Permission { Id = 3, Name = "Permission3", Description = "Permission3", Type = PermissionType.Global },
                });

                context.SaveChanges();
            }
        }

        [Fact]
        public async Task Handle_GivenValidRequests_GrantsPermissions()
        {
            // Arrange
            var sut = new GrantRolePermissionsCommandHandler(_context);
            var command = new GrantRolePermissionsCommand
            {
                SchemeId = 1,
                RoleId = 1,
                PermissionIds = new int[] { 1, 2 }
            };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var permissions = _context.PermissionSchemeRolePermissions
                .Where(s => s.PermissionSchemeId == 1).Where(s => s.RoleId == 1).Select(s => s.Permission).ToList();

            // Assert
            result.Succeeded.ShouldBe(true);
            permissions.Count.ShouldBe(2);
            permissions.Select(p => p.Id).ShouldContain(1);
            permissions.Select(p => p.Id).ShouldContain(2);
        }

        [Fact]
        public async Task Handle_GivenNoPermissions_ClearsAllPermissions()
        {
            // Arrange
            using (var context = CreateContext())
            {
                context.PermissionSchemeRolePermissions.Add(new PermissionSchemeRolePermission { PermissionSchemeId = 1, RoleId = 1, PermissionId = 1 });
                context.SaveChanges();
            }

            var sut = new GrantRolePermissionsCommandHandler(_context);
            var command = new GrantRolePermissionsCommand { SchemeId = 1, RoleId = 1, PermissionIds = new List<int>() };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var permissions = _context.PermissionSchemeRolePermissions
                .Where(s => s.PermissionSchemeId == 1).Where(s => s.RoleId == 1).Select(s => s.Permission).ToList();

            // Assert
            result.Succeeded.ShouldBe(true);
            permissions.Count.ShouldBe(0);
        }

        [Fact]
        public async Task Handle_GivenNewPermissions_ReplacesOldPermissions()
        {
            // Arrange
            using (var context = CreateContext())
            {
                context.PermissionSchemeRolePermissions.Add(new PermissionSchemeRolePermission { PermissionSchemeId = 1, RoleId = 1, PermissionId = 1 });
                context.SaveChanges();
            }

            var sut = new GrantRolePermissionsCommandHandler(_context);
            var command = new GrantRolePermissionsCommand { SchemeId = 1, RoleId = 1, PermissionIds = new int[] { 2, 3 } };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var permissions = _context.PermissionSchemeRolePermissions
                .Where(s => s.PermissionSchemeId == 1).Where(s => s.RoleId == 1).Select(s => s.Permission).ToList();

            // Assert
            result.Succeeded.ShouldBe(true);
            permissions.Count.ShouldBe(2);
            permissions.Select(p => p.Id).ShouldContain(2);
            permissions.Select(p => p.Id).ShouldContain(3);
        }
    }
}