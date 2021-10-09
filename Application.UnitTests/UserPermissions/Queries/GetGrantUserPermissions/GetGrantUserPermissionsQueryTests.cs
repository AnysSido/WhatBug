using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.UnitTests.Common;
using WhatBug.Application.UserPermissions.Queries.GetGrantUserPermissions;
using Xunit;

namespace WhatBug.Application.UnitTests.UserPermissions.Queries.GetGrantUserPermissions
{
    [Collection("QueryCollection")]
    public class GetGrantUserPermissionsQueryTests
    {
        private readonly IWhatBugDbContext _context;

        public GetGrantUserPermissionsQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.CreateContext();
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsAllPermissionsOfTypeUser()
        {
            // Arrange
            var sut = new GetGrantUserPermissionsQueryHandler(_context);

            // Act
            var result = await sut.Handle(new GetGrantUserPermissionsQuery { UserId = 1 }, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Permissions.Count.ShouldBe(3);
            result.Result.Permissions.Select(p => p.Id).ShouldContain(1);
            result.Result.Permissions.Select(p => p.Id).ShouldContain(2);
            result.Result.Permissions.Select(p => p.Id).ShouldContain(3);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(3, 1)]
        public async Task Handle_GivenValidRequest_MarksExistingUserPermissionsAsGranted(int userId, int numGranted)
        {
            // Arrange
            var sut = new GetGrantUserPermissionsQueryHandler(_context);

            // Act
            var result = await sut.Handle(new GetGrantUserPermissionsQuery { UserId = userId }, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Permissions.Count.ShouldBe(3);
            result.Result.Permissions.Where(p => p.IsGranted).Count().ShouldBe(numGranted);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_IncludesUserAndPermissionDetails()
        {
            // Arrange
            var sut = new GetGrantUserPermissionsQueryHandler(_context);

            // Act
            var result = await sut.Handle(new GetGrantUserPermissionsQuery { UserId = 1 }, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Id.ShouldBe(1);
            result.Result.Username.ShouldBe("TestUser1");

            result.Result.Permissions[0].Id.ShouldBe(1);
            result.Result.Permissions[0].Name.ShouldBe("Permission1");
            result.Result.Permissions[0].Description.ShouldBe("PermissionDesc1");

            result.Result.Permissions[1].Id.ShouldBe(2);
            result.Result.Permissions[1].Name.ShouldBe("Permission2");
            result.Result.Permissions[1].Description.ShouldBe("PermissionDesc2");
        }
    }
}