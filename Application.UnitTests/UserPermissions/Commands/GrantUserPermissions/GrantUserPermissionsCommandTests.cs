using Application.UnitTests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.UserPermissions.Commands.GrantUserPermissions;
using WhatBug.Domain.Entities;
using WhatBug.Domain.Entities.JoinTables;
using Xunit;

namespace WhatBug.Application.UnitTests.UserPermissions.Commands.GrantUserPermissions
{
    public class GrantUserPermissionsCommandTests : CommandTestBase
    {
        public GrantUserPermissionsCommandTests()
        {
            using (var context = CreateContext())
            {
                context.Users.AddRange(new[]
                {
                    new User { Id = 1 },
                    new User { Id = 2 }
                });

                context.Permissions.AddRange(new Permission[]
                {
                    new Permission { Id = 1, Name = "Permission1", Description = "Permission1", Type = PermissionType.Global },
                    new Permission { Id = 2, Name = "Permission2", Description = "Permission2", Type = PermissionType.Global },
                    new Permission { Id = 3, Name = "Permission3", Description = "Permission3", Type = PermissionType.Project },
                });

                context.SaveChanges();
            }
        }

        [Fact]
        public async Task Handle_GivenValidRequest_GrantsPermissions()
        {
            // Arrange
            var sut = new GrantUserPermissionsCommandHandler(_context);
            var command = new GrantUserPermissionsCommand
            {
                UserId = 1,
                PermissionIds = new int[] { 1, 2 }
            };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var permissions = _context.Users.Include(u => u.UserPermissions).Single(u => u.Id == 1).UserPermissions;

            // Assert
            result.Succeeded.ShouldBe(true);
            permissions.Count.ShouldBe(2);
            permissions.Select(p => p.Permission.Id).ShouldContain(1);
            permissions.Select(p => p.Permission.Id).ShouldContain(2);
        }

        [Fact]
        public async Task Handle_GivenNoPermissions_ClearsAllPermissions()
        {
            // Arrange
            using (var context = CreateContext())
            {
                context.Users.Include(u => u.UserPermissions)
                    .Single(u => u.Id == 1).UserPermissions.Add(new UserPermission { UserId = 1, PermissionId = 1 });
                context.SaveChanges();
            }

            var sut = new GrantUserPermissionsCommandHandler(_context);
            var command = new GrantUserPermissionsCommand { UserId = 1 };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var permissions = _context.Users.Include(u => u.UserPermissions).Single(u => u.Id == 1).UserPermissions;

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
                context.Users.Include(u => u.UserPermissions)
                    .Single(u => u.Id == 1).UserPermissions.Add(new UserPermission { UserId = 1, PermissionId = 1 });
                context.SaveChanges();
            }

            var sut = new GrantUserPermissionsCommandHandler(_context);
            var command = new GrantUserPermissionsCommand
            {
                UserId = 1,
                PermissionIds = new int[] { 2, 3 }
            };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var permissions = _context.Users.Include(u => u.UserPermissions).Single(u => u.Id == 1).UserPermissions;

            // Assert
            result.Succeeded.ShouldBe(true);
            permissions.Count.ShouldBe(2);
            permissions.Select(p => p.Permission.Id).ShouldContain(2);
            permissions.Select(p => p.Permission.Id).ShouldContain(3);
        }
    }
}