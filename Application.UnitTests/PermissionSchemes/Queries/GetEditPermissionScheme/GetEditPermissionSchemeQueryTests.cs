using AutoMapper;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.PermissionSchemes.Queries.GetEditPermissionScheme;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Queries.GetEditPermissionScheme
{
    [Collection("QueryCollection")]
    public class GetEditPermissionSchemeQueryTests
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetEditPermissionSchemeQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.CreateContext();
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsPermissionScheme()
        {
            // Arrange
            var sut = new GetEditPermissionSchemeQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetEditPermissionSchemeQuery { SchemeId = 1 }, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.Id.ShouldBe(1);
            result.Result.Name.ShouldBe("PermissionScheme1");
            result.Result.Description.ShouldBe("PermissionSchemeDesc1");
        }
    }
}