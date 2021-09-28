using AutoMapper;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.ProjectRoles.Queries.GetRoles;
using WhatBug.Application.UnitTests.Common;
using WhatBug.Persistence;
using Xunit;

namespace WhatBug.Application.UnitTests.ProjectRoles.Queries.GetRoles
{
    [Collection("QueryCollection")]
    public class GetRolesQueryTests
    {
        private readonly WhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetRolesQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsRoles()
        {
            var sut = new GetRolesQueryHandler(_context, _mapper);

            var result = await sut.Handle(new GetRolesQuery(), CancellationToken.None);

            result.Succeeded.ShouldBe(true);
            result.Result.Roles.ShouldNotBeNull();
            result.Result.Roles.Count.ShouldBe(3);
            result.Result.Roles[0].Name.ShouldBe("Admin");
            result.Result.Roles[0].Description.ShouldBe("Admin Role");
            result.Result.Roles[1].Name.ShouldBe("Developer");
            result.Result.Roles[2].Name.ShouldBe("QA");
        }
    }
}