using AutoMapper;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.PrioritySchemes.Queries.GetCreatePriorityScheme;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.PrioritySchemes.Queries.GetCreatePriorityScheme
{
    [Collection("QueryCollection")]
    public class GetCreatePrioritySchemeQueryTests
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetCreatePrioritySchemeQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.CreateContext();
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsPriorities()
        {
            // Arrange
            var sut = new GetCreatePrioritySchemeQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetCreatePrioritySchemeQuery(), CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.PriorityIds.ShouldBeNull();
            result.Result.Priorities.Count.ShouldBe(4);
            result.Result.Priorities.Select(p => p.Id).OrderBy(p => p).ToArray().ShouldBeEquivalentTo(new[] { 1, 2, 3, 4 });
        }
    }
}