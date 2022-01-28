using AutoMapper;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.UnitTests.Common;
using WhatBug.Application.UserPermissions.Queries.GetUsersAndPermissions;
using Xunit;

namespace WhatBug.Application.UnitTests.UserPermissions.Queries.GetUsersAndPermissions
{
    [Collection("QueryCollection")]
    public class GetUsersAndPermissionsQueryTests
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersAndPermissionsQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.CreateContext();
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsUsersAndPermissions()
        {
            // Arrange
            var sut = new GetUsersAndPermissionsQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetUsersAndPermissionsQuery(), CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Users.ShouldNotBeNull();
            result.Result.Users.Count.ShouldBe(3);

            var user1 = result.Result.Users.Single(u => u.Id == 1);
            user1.Username.ShouldBe("TestUser1");
            user1.FirstName.ShouldBe("FirstName1");
            user1.Surname.ShouldBe("Surname1");
            user1.Permissions.Count.ShouldBe(2);

            var user2 = result.Result.Users.Single(u => u.Id == 2);
            user2.Username.ShouldBe("TestUser2");
            user2.Permissions.Count.ShouldBe(0);

            var user3 = result.Result.Users.Single(u => u.Id == 3);
            user3.Username.ShouldBe("TestUser3");
            user3.Permissions.Count.ShouldBe(1);
        }
    }
}