using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.PermissionSchemes.Queries.GetGrantRolePermissions;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Queries.GetGrantRolePermissions
{
    [Collection("QueryCollection")]
    public class GetGrantRolePermissionsQueryTests
    {
        private readonly IWhatBugDbContext _context;

        public GetGrantRolePermissionsQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.CreateContext();
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsAllPermissionsOfTypeProject()
        {
            // Arrange
            var sut = new GetGrantRolePermissionsQueryHandler(_context);

            // Act
            var result = await sut.Handle(new GetGrantRolePermissionsQuery { SchemeId = 1, RoleId = 3 }, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Permissions.Count.ShouldBe(3);
            result.Result.Permissions.Select(p => p.Id).ShouldContain(4);
            result.Result.Permissions.Select(p => p.Id).ShouldContain(5);
            result.Result.Permissions.Select(p => p.Id).ShouldContain(6);
        }

        [Theory]
        [InlineData(1, 1, 3)]
        [InlineData(1, 2, 2)]
        [InlineData(1, 3, 0)]
        public async Task Handle_GivenValidRequest_MarksExistingRolePermissionsAsGranted(int schemeId, int roleId, int numGranted)
        {
            // Arrange
            var sut = new GetGrantRolePermissionsQueryHandler(_context);

            // Act
            var result = await sut.Handle(new GetGrantRolePermissionsQuery { SchemeId = schemeId, RoleId = roleId }, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Permissions.Where(p => p.IsGranted).Count().ShouldBe(numGranted);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsSchemeWithRoleAndPermissions()
        {
            // Arrange
            var sut = new GetGrantRolePermissionsQueryHandler(_context);

            // Act
            var result = await sut.Handle(new GetGrantRolePermissionsQuery { SchemeId = 1, RoleId = 2 }, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Id.ShouldBe(1);
            result.Result.Name.ShouldBe("PermissionScheme1");
            result.Result.Description.ShouldBe("PermissionSchemeDesc1");
            result.Result.RoleId.ShouldBe(2);
            result.Result.RoleName.ShouldBe("Developer");
            result.Result.Permissions.Count.ShouldBe(3);

            result.Result.Permissions[0].Id.ShouldBe(4);
            result.Result.Permissions[0].Name.ShouldBe("Permission4");
            result.Result.Permissions[0].Description.ShouldBe("PermissionDesc4");

            result.Result.Permissions[1].Id.ShouldBe(5);
            result.Result.Permissions[1].Name.ShouldBe("Permission5");
            result.Result.Permissions[1].Description.ShouldBe("PermissionDesc5");
        }
    }
}