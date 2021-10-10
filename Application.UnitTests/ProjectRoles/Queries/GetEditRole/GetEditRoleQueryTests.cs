using AutoMapper;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.ProjectRoles.Queries.GetEditRole;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.ProjectRoles.Queries.GetEditRole
{
    [Collection("QueryCollection")]
    public class GetEditRoleQueryTests
    {
        private readonly IWhatBugDbContext _context;
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

            // Act
            var result = await sut.Handle(new GetEditRoleQuery { RoleId = 1 }, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Id.ShouldBe(1);
            result.Result.Name.ShouldBe("Admin");
            result.Result.Description.ShouldBe("Admin Role");
        }
    }
}