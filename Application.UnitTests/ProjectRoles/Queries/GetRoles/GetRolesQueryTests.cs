using AutoMapper;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.ProjectRoles.Queries.GetRoles;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.ProjectRoles.Queries.GetRoles
{
    [Collection("QueryCollection")]
    public class GetRolesQueryTests
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetRolesQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.CreateContext();
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsRoles()
        {
            // Arrange
            var sut = new GetRolesQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetRolesQuery(), CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Roles.ShouldNotBeNull();
            result.Result.Roles.Count.ShouldBe(3);

            var role = result.Result.Roles.SingleOrDefault(r => r.Name == "Admin");
            role.ShouldNotBeNull();
            role.Projects.Count.ShouldBe(2);

            var project = role.Projects.SingleOrDefault(p => p.Name == "Test Project 1");
            project.ShouldNotBeNull();
            project.Users.ShouldNotBeNull();
            project.Users.Count.ShouldBe(3);
            project.Users.Select(u => u.Username).ShouldContain("TestUser1");
            project.Users.Select(u => u.Username).ShouldContain("TestUser2");
            project.Users.Select(u => u.Username).ShouldContain("TestUser3");

            project = role.Projects.SingleOrDefault(p => p.Name == "Test Project 3");
            project.ShouldNotBeNull();
            project.Users.ShouldNotBeNull();
            project.Users.Count.ShouldBe(2);
            project.Users.Select(u => u.Username).ShouldContain("TestUser1");
            project.Users.Select(u => u.Username).ShouldContain("TestUser2");

            role = result.Result.Roles.SingleOrDefault(r => r.Name == "Developer");
            role.ShouldNotBeNull();
            role.Projects.Count.ShouldBe(0);

            role = result.Result.Roles.SingleOrDefault(r => r.Name == "QA");
            role.ShouldNotBeNull();
            role.Projects.Count.ShouldBe(2);

            project = role.Projects.SingleOrDefault(p => p.Name == "Test Project 1");
            project.ShouldNotBeNull();
            project.Users.ShouldNotBeNull();
            project.Users.Count.ShouldBe(1);
            project.Users.Select(u => u.Username).ShouldContain("TestUser3");

            project = role.Projects.SingleOrDefault(p => p.Name == "Test Project 3");
            project.ShouldNotBeNull();
            project.Users.ShouldNotBeNull();
            project.Users.Count.ShouldBe(2);
            project.Users.Select(u => u.Username).ShouldContain("TestUser2");
            project.Users.Select(u => u.Username).ShouldContain("TestUser3");
        }
    }
}