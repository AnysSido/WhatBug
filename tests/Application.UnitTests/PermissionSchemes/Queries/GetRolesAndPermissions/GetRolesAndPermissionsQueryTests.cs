using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.PermissionSchemes.Queries.GetRolesAndPermissions;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Queries.GetRolesAndPermissions
{
    [Collection("QueryCollection")]
    public class GetRolesAndPermissionsQueryTests
    {
        private readonly IWhatBugDbContext _context;

        public GetRolesAndPermissionsQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.CreateContext();
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsRolesWithPermissions()
        {
            // Arrange
            var sut = new GetRolesAndPermissionsQueryHandler(_context);

            // Act
            var result = await sut.Handle(new GetRolesAndPermissionsQuery { SchemeId = 1 }, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Roles.Count.ShouldBe(3);
            result.Result.SchemeId.ShouldBe(1);

            var role = result.Result.Roles.FirstOrDefault(r => r.Id == 1);
            role.ShouldNotBeNull();
            role.Permissions.Count.ShouldBe(3);
            role.Permissions.Select(p => p.Id).ShouldContain(4);
            role.Permissions.Select(p => p.Id).ShouldContain(5);
            role.Permissions.Select(p => p.Id).ShouldContain(6);

            role = result.Result.Roles.FirstOrDefault(r => r.Id == 2);
            role.ShouldNotBeNull();
            role.Permissions.Count.ShouldBe(2);
            role.Permissions.Select(p => p.Id).ShouldContain(4);
            role.Permissions.Select(p => p.Id).ShouldContain(5);
        }
    }
}