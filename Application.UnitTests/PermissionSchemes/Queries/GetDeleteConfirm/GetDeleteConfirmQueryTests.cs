using AutoMapper;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.PermissionSchemes.Queries.GetDeleteConfirm;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Queries.GetDeleteConfirm
{
    [Collection("QueryCollection")]
    public class GetDeleteConfirmQueryTests
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetDeleteConfirmQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.CreateContext();
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsSchemeInfo()
        {
            // Arrange
            var sut = new GetDeleteConfirmQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetDeleteConfirmQuery { SchemeId = 2 }, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.SchemeId.ShouldBe(2);
            result.Result.Name.ShouldBe("PermissionScheme2");
            result.Result.Description.ShouldBe("PermissionSchemeDesc2");
        }
    }
}