using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Domain.Entities;
using WhatBug.Persistence;

namespace WhatBug.Application.UnitTests.Common
{
    public class WhatBugContextFactory
    {
        private DbContextOptions<WhatBugDbContext> _options;
        private ICurrentUserService _currentUserService;

        public IWhatBugDbContext Create(string guid)
        {
            if (_options == null)
            {
                _options = new DbContextOptionsBuilder<WhatBugDbContext>()
                    .UseInMemoryDatabase(guid).Options;

                var mockUserService = new Mock<ICurrentUserService>();
                mockUserService.SetupGet(x => x.Id).Returns(1);
                _currentUserService = mockUserService.Object;
            }

            return new WhatBugDbContext(_options, _currentUserService);
        }

        public IWhatBugDbContext CreateWithSeed(string guid)
        {
            var context = Create(guid);

            context.Roles.AddRange(new[]
            {
                new Role { Id = 1, Name = "Admin", Description = "Admin Role"},
                new Role { Id = 2, Name = "Developer", Description = "Developer Role"},
                new Role { Id = 3, Name = "QA", Description = "QA Role"},
            });

            context.Projects.AddRange(new[]
            {
                new Project { Id = 1, Name = "Test Project 1", Description = "Test Proj 1" },
                new Project { Id = 2, Name = "Test Project 2", Description = "Test Proj 2" },
                new Project { Id = 3, Name = "Test Project 3", Description = "Test Proj 3" }
            });

            context.Users.AddRange(new[]
            {
                new User {Id = 1, Username = "TestUser1", FirstName = "FirstName1", Surname = "Surname1", Email = "TestUser1@whatbug.com" },
                new User {Id = 2, Username = "TestUser2", FirstName = "FirstName2", Surname = "Surname2", Email = "Test.User2@whatbug.com" },
                new User {Id = 3, Username = "TestUser3", FirstName = "FirstName3", Surname = "Surname3", Email = "Test_User3@whatbug.com" },
            });

            context.Permissions.AddRange(new[]
            {
                new Permission { Id = 1, Name = "Permission1", Description = "PermissionDesc1", Type = PermissionType.Global },
                new Permission { Id = 2, Name = "Permission2", Description = "PermissionDesc2", Type = PermissionType.Global },
                new Permission { Id = 3, Name = "Permission3", Description = "PermissionDesc3", Type = PermissionType.Global },
                new Permission { Id = 4, Name = "Permission4", Description = "PermissionDesc4", Type = PermissionType.Project },
                new Permission { Id = 5, Name = "Permission5", Description = "PermissionDesc5", Type = PermissionType.Project },
                new Permission { Id = 6, Name = "Permission6", Description = "PermissionDesc6", Type = PermissionType.Project },
            });

            context.UserPermissions.AddRange(new[]
            {
                new UserPermission { UserId = 1, PermissionId = 1 },
                new UserPermission { UserId = 1, PermissionId = 2 },
                new UserPermission { UserId = 3, PermissionId = 2 },
            });

            context.PermissionSchemes.AddRange(new[]
            {
                new PermissionScheme { Id = 1, Name = "PermissionScheme1", Description = "PermissionSchemeDesc1", IsDefault = true },
                new PermissionScheme { Id = 2, Name = "PermissionScheme2", Description = "PermissionSchemeDesc2" },
                new PermissionScheme { Id = 3, Name = "PermissionScheme3", Description = "PermissionSchemeDesc3" },
            });

            context.PermissionSchemeRolePermissions.AddRange(new[]
            {
                new PermissionSchemeRolePermission { PermissionSchemeId = 1, RoleId = 1, PermissionId = 4 },
                new PermissionSchemeRolePermission { PermissionSchemeId = 1, RoleId = 1, PermissionId = 5 },
                new PermissionSchemeRolePermission { PermissionSchemeId = 1, RoleId = 1, PermissionId = 6 },
                new PermissionSchemeRolePermission { PermissionSchemeId = 1, RoleId = 2, PermissionId = 4 },
                new PermissionSchemeRolePermission { PermissionSchemeId = 1, RoleId = 2, PermissionId = 5 },
            });

            context.Colors.AddRange(new[]
            {
                new Color { Id = 1, Name = "Color1" },
                new Color { Id = 2, Name = "Color2" },
                new Color { Id = 3, Name = "Color3" },
            });

            context.Icons.AddRange(new[]
            {
                new Icon { Id = 1, Name = "Icon1", WebName = "icon1"},
                new Icon { Id = 2, Name = "Icon2", WebName = "icon2"},
                new Icon { Id = 3, Name = "Icon3", WebName = "icon3"},
            });

            context.Priorities.AddRange(new[]
            {
                new Priority { Id = 1, Name = "Priority1", Description = "PriorityDesc1", Order = 4, ColorId = 1, IconId = 1 },
                new Priority { Id = 2, Name = "Priority2", Description = "PriorityDesc2", Order = 3, ColorId = 1, IconId = 1 },
                new Priority { Id = 3, Name = "Priority3", Description = "PriorityDesc3", Order = 2, ColorId = 2, IconId = 2 },
                new Priority { Id = 4, Name = "Priority4", Description = "PriorityDesc4", Order = 1, ColorId = 3, IconId = 3, IsDefault = true },
            });

            context.SaveChanges();

            context.Projects.First().RoleUsers = new List<ProjectRoleUser>
            {
                new ProjectRoleUser { ProjectId = 1, RoleId = 1, UserId = 1 },
                new ProjectRoleUser { ProjectId = 1, RoleId = 1, UserId = 2 },
                new ProjectRoleUser { ProjectId = 1, RoleId = 1, UserId = 3 },
                new ProjectRoleUser { ProjectId = 1, RoleId = 3, UserId = 3 },
            };

            context.Projects.Last().RoleUsers = new List<ProjectRoleUser>
            {
                new ProjectRoleUser { ProjectId = 3, RoleId = 1, UserId = 1 },
                new ProjectRoleUser { ProjectId = 3, RoleId = 1, UserId = 2 },
                new ProjectRoleUser { ProjectId = 3, RoleId = 3, UserId = 2 },
                new ProjectRoleUser { ProjectId = 3, RoleId = 3, UserId = 3 },
            };

            context.SaveChanges();

            return context;
        }

        public static void Dispose(IWhatBugDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}