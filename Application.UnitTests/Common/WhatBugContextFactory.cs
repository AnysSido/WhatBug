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

        public WhatBugDbContext Create(string guid)
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

        public WhatBugDbContext CreateWithSeed(string guid)
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

        public static void Dispose(WhatBugDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
