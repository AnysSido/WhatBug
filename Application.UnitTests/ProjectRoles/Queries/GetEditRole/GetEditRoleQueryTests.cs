using AutoMapper;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.ProjectRoles.Queries.GetEditRole;
using WhatBug.Application.UnitTests.Common;
using WhatBug.Persistence;
using Xunit;

namespace WhatBug.Application.UnitTests.ProjectRoles.Queries.GetEditRole
{
    [Collection("QueryCollection")]
    public class GetEditRoleQueryTests
    {
        private readonly WhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetEditRoleQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.CreateContext();
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsRole()
        {
            // Arrange
            var sut = new GetEditRoleQueryHandler(_context, _mapper);
            var roleId = 1;

            // Act
            var result = await sut.Handle(new GetEditRoleQuery { RoleId = roleId }, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Id.ShouldBe(roleId);
            result.Result.Name.ShouldBe("Admin");
            result.Result.Description.ShouldBe("Admin Role");
        }
    }
}