using AutoMapper;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.ProjectRoles.Queries.GetDeleteRoleConfirm;
using WhatBug.Application.UnitTests.Common;
using WhatBug.Persistence;
using Xunit;

namespace WhatBug.Application.UnitTests.ProjectRoles.Queries.GetDeleteRoleConfirm
{
    [Collection("QueryCollection")]
    public class GetDeleteRoleConfirmQueryTests
    {
        private readonly WhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetDeleteRoleConfirmQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_GivenRoleIdWithProjects_ReturnsRoleWithProjectsAndUsers()
        {
            // Arrange
            var sut = new GetDeleteRoleConfirmQueryHandler(_context, _mapper);
            var roleId = 1;

            // Act
            var result = await sut.Handle(new GetDeleteRoleConfirmQuery { RoleId = roleId }, CancellationToken.None);
            var project1 = result.Result.ProjectsUsingRole.Single(p => p.Id == 1);
            var project2 = result.Result.ProjectsUsingRole.Single(p => p.Id == 3);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Name.ShouldBe("Admin");
            result.Result.ProjectsUsingRole.Count.ShouldBe(2);

            project1.ShouldNotBeNull();
            project1.Name.ShouldBe("Test Project 1");
            project1.Users.Count.ShouldBe(3);
            project1.Users.Select(u => u.FirstName).ShouldContain("FirstName1");
            project1.Users.Select(u => u.FirstName).ShouldContain("FirstName2");
            project1.Users.Select(u => u.FirstName).ShouldContain("FirstName3");

            project2.ShouldNotBeNull();
            project2.Name.ShouldBe("Test Project 3");
            project2.Users.Count.ShouldBe(2);
            project2.Users.Select(u => u.FirstName).ShouldContain("FirstName1");
            project2.Users.Select(u => u.FirstName).ShouldContain("FirstName2");
        }

        [Fact]
        public async Task Handle_GivenRoleIdWithoutProjects_ReturnsRoleWithoutProjects()
        {
            // Arrange
            var sut = new GetDeleteRoleConfirmQueryHandler(_context, _mapper);
            var roleId = 2;

            // Act
            var result = await sut.Handle(new GetDeleteRoleConfirmQuery { RoleId = roleId }, CancellationToken.None);

            result.Result.Name.ShouldBe("Developer");
            result.Succeeded.ShouldBe(true);
            result.Result.ProjectsUsingRole.ShouldBeEmpty();
        }
    }
}